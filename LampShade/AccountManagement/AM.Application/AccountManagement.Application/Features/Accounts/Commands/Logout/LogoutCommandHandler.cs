using _0_Framework.Application;
using AccountManagement.Application.Contracts.Commands.Accounts.Logout;
using MediatR;

namespace AccountManagement.Application.Features.Accounts.Commands.Logout;

public class LogoutCommandHandler(IAuthHelper authHelper) : IRequestHandler<LogoutCommand, OperationResult>
{
    public Task<OperationResult> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        authHelper.SignOut();
        return Task.FromResult(new OperationResult().Succeeded());
    }
}
