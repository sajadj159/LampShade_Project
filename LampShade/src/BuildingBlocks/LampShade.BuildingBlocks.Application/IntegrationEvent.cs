using MediatR;

namespace LampShade.BuildingBlocks.Application;

/// <summary>
/// A fact one module publishes for other modules. Integration events are written to the
/// publishing module's outbox in the same transaction as its data and delivered afterwards,
/// so they are part of a module's public contract and must stay backwards compatible.
/// </summary>
public abstract record IntegrationEvent : INotification
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
}
