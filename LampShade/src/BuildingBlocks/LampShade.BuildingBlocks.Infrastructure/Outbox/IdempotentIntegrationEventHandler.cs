using LampShade.BuildingBlocks.Application;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LampShade.BuildingBlocks.Infrastructure.Outbox;

/// <summary>
/// Base class for a module reacting to another module's integration event. The reaction and the
/// inbox record are saved in one transaction of the consumer's own context, so a redelivered
/// event is skipped and a failed reaction leaves no trace.
/// </summary>
public abstract class IdempotentIntegrationEventHandler<TEvent, TContext>(TContext context) : INotificationHandler<TEvent>
    where TEvent : IntegrationEvent
    where TContext : DbContext
{
    public async Task Handle(TEvent notification, CancellationToken cancellationToken)
    {
        var handlerName = GetType().FullName!;
        var alreadyHandled = await context.Set<InboxMessage>()
            .AnyAsync(x => x.EventId == notification.Id && x.Handler == handlerName, cancellationToken);
        if (alreadyHandled)
            return;

        await HandleCoreAsync(notification, cancellationToken);

        context.Set<InboxMessage>().Add(new InboxMessage(notification.Id, handlerName));
        await context.SaveChangesAsync(cancellationToken);
    }

    protected abstract Task HandleCoreAsync(TEvent integrationEvent, CancellationToken cancellationToken);
}
