namespace LampShade.BuildingBlocks.Application;

public interface ICommitAwareResponse
{
    void OnCommitted();
}