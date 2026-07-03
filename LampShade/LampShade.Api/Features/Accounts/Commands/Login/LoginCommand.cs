using AccountManagement.Application.Contracts.AC.Account;
using MediatR;
using _0_Framework.Application;

namespace LampShade.Api.Features.Accounts.Commands.Login;

public class LoginCommand : IRequest<OperationResult>
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, OperationResult>
{
    private readonly IAccountApplication _accountApplication;

    public LoginCommandHandler(IAccountApplication accountApplication)
    {
        _accountApplication = accountApplication;
    }

    public async Task<OperationResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var command = new AccountManagement.Application.Contracts.AC.Account.Login
        {
            UserName = request.UserName,
            Password = request.Password
        };
        return await Task.FromResult(_accountApplication.Login(command));
    }
}
