using AccountManagement.Application.Contracts.AC.Account;
using MediatR;
using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.Commands.Accounts.Login;

public class LoginCommand : IRequest<OperationResult>
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
