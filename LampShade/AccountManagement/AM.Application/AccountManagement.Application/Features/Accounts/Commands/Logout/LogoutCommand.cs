using AccountManagement.Application.Contracts.AC.Account;
using MediatR;
using _0_Framework.Application;

namespace AccountManagement.Application.Features.Accounts.Commands.Logout;

public class LogoutCommand : IRequest<OperationResult> { }

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, OperationResult>
{
    private readonly IAccountApplication _accountApplication;

    public LogoutCommandHandler(IAccountApplication accountApplication)
    {
        _accountApplication = accountApplication;
    }

    public Task<OperationResult> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        _accountApplication.Logout();
        return Task.FromResult(new OperationResult().Succeeded());
    }
}
