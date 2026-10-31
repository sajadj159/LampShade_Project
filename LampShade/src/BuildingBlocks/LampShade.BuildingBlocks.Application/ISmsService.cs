namespace LampShade.BuildingBlocks.Application
{
    public interface ISmsService
    {
        void Send(string nummber, string message);
    }
}