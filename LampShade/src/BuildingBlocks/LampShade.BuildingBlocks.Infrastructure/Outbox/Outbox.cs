using LampShade.BuildingBlocks.Application;
using Microsoft.EntityFrameworkCore;

namespace LampShade.BuildingBlocks.Infrastructure.Outbox;

/// <summary>Writes integration events into <typeparamref name="TContext"/>'s outbox table; saved by the unit of work.</summary>
public class Outbox<TContext>(TContext context) : IOutbox where TContext : DbContext
{
    public void Add(IntegrationEvent integrationEvent) => context.Set<OutboxMessage>().Add(OutboxMessage.From(integrationEvent));
}
