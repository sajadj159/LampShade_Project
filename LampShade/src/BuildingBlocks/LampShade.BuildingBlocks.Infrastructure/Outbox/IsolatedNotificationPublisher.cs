using MediatR;

namespace LampShade.BuildingBlocks.Infrastructure.Outbox;

/// <summary>
/// Delivers an integration event to every subscribed module even when one of them fails, then reports
/// all failures together. With MediatR's default publisher a failing subscriber (e.g. Account's SMS)
/// would stop the ones registered after it (e.g. Inventory's stock reduction). The outbox retries the
/// event and the inbox keeps the subscribers that already succeeded from running twice.
/// </summary>
public sealed class IsolatedNotificationPublisher : INotificationPublisher
{
    public async Task Publish(IEnumerable<NotificationHandlerExecutor> handlerExecutors, INotification notification, CancellationToken cancellationToken)
    {
        List<Exception>? failures = null;
        foreach (var handler in handlerExecutors)
        {
            try
            {
                await handler.HandlerCallback(notification, cancellationToken);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                (failures ??= []).Add(ex);
            }
        }

        if (failures is { Count: 1 }) throw failures[0];
        if (failures is { Count: > 1 }) throw new AggregateException($"{failures.Count} subscribers of {notification.GetType().Name} failed.", failures);
    }
}
