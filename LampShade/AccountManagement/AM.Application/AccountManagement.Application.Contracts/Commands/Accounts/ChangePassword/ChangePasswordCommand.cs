using AccountManagement.Application.Contracts.AC.Account;
using MediatR;
using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.Commands.Accounts.ChangePassword;

public class ChangePasswordCommand : ICommand<OperationResult>
{
    public long Id { get; set; }
    public string Password { get; set; } = string.Empty;
    public string RePassword { get; set; } = string.Empty;
}
