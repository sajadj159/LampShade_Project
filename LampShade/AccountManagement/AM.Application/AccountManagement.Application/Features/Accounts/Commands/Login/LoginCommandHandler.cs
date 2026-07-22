using AccountManagement.Application.Contracts.AC.Account;
using MediatR;
using _0_Framework.Application;

using AccountManagement.Application.Contracts.Commands.Accounts.Login;

namespace AccountManagement.Application.Features.Accounts.Commands.Login;



public class LoginCommandHandler : IRequestHandler<LoginCommand, OperationResult>
{
    private readonly IAccountApplication _accountApplication;

    public LoginCommandHandler(IAccountApplication accountApplication)
    {
        _accountApplication = accountApplication;
    }

    public Task<OperationResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var command = new AccountManagement.Application.Contracts.AC.Account.Login
        {
            UserName = request.UserName,
            Password = request.Password
        };
        return Task.FromResult(_accountApplication.Login(command));
    }
}
