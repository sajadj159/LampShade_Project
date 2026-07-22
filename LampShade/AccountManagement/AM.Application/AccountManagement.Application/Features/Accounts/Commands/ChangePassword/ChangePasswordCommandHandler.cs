using AccountManagement.Application.Contracts.AC.Account;
using MediatR;
using _0_Framework.Application;

using AccountManagement.Application.Contracts.Commands.Accounts.ChangePassword;

namespace AccountManagement.Application.Features.Accounts.Commands.ChangePassword;



public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, OperationResult>
{
    private readonly IAccountApplication _accountApplication;

    public ChangePasswordCommandHandler(IAccountApplication accountApplication)
    {
        _accountApplication = accountApplication;
    }

    public Task<OperationResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var command = new AccountManagement.Application.Contracts.AC.Account.ChangePassword
        {
            Id = request.Id,
            Password = request.Password,
            RePassword = request.RePassword
        };
        return Task.FromResult(_accountApplication.ChangePassword(command));
    }
}
