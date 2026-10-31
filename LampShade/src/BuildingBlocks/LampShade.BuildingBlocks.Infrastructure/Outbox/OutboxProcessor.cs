using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LampShade.BuildingBlocks.Infrastructure.Outbox;

/// <summary>
/// Publishes the pending integration events of one module, in order, to every subscribed module.
/// A failed event stays pending and is retried on the next poll; consumers stay idempotent
/// through <see cref="IdempotentIntegrationEventHandler{TEvent,TContext}"/>.
/// </summary>
public sealed class OutboxProcessor<TContext>(IServiceScopeFactory scopeFactory, ILogger<OutboxProcessor<TContext>> logger)
    : BackgroundService where TContext : DbContext
{
    private const int BatchSize = 20;
    private const int MaxAttempts = 10;
    private static readonly TimeSpan PollInterval = TimeSpan.FromSeconds(5);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await ProcessPendingAsync(stoppingToken);
            }
            catch (Exception ex) when (!stoppingToken.IsCancellationRequested)
            {
                // The schema may not exist yet while the host is still migrating; try again next poll.
                logger.LogDebug(ex, "Outbox poll for {Context} failed", typeof(TContext).Name);
            }

            await Task.Delay(PollInterval, stoppingToken);
        }
    }

    public async Task<int> ProcessPendingAsync(CancellationToken cancellationToken)
    {
        await using var scope = scopeFactory.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<TContext>();
        var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();

        var messages = await context.Set<OutboxMessage>()
            .Where(x => x.ProcessedOn == null && x.Attempts < MaxAttempts)
            .OrderBy(x => x.OccurredOn)
            .Take(BatchSize)
            .ToListAsync(cancellationToken);

        foreach (var message in messages)
        {
            try
            {
                await publisher.Publish(message.ToIntegrationEvent(), cancellationToken);
                message.MarkProcessed();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Publishing outbox message {MessageId} ({Type}) failed", message.Id, message.Type);
                message.MarkFailed(ex.Message);
            }

            await context.SaveChangesAsync(cancellationToken);
        }

        return messages.Count;
    }
}
