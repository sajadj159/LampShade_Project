using MediatR;
using LampShade.BuildingBlocks.Application;

namespace AccountManagement.Application.Contracts.Commands.Accounts.Login;

public class LoginCommand : ICommand<OperationResult>
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
