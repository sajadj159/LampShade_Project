namespace LampShade.BuildingBlocks.Application;

/// <summary>
/// Queues an integration event in the current module's outbox. The event is stored with the
/// module's other changes when the command commits and is published only after that.
/// </summary>
public interface IOutbox
{
    void Add(IntegrationEvent integrationEvent);
}
