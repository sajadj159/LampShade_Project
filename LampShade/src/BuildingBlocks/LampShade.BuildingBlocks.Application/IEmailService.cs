namespace LampShade.BuildingBlocks.Application;

public interface IEmailService
{
    void SendEmail(string title, string messageBody, string destination);
}
