using AccountManagement.Application.Contracts.AC.Account;
using MediatR;
using _0_Framework.Application;

namespace AccountManagement.Application.Features.Accounts.Commands.ChangePassword;

public class ChangePasswordCommand : IRequest<OperationResult>
{
    public long Id { get; set; }
    public string Password { get; set; } = string.Empty;
    public string RePassword { get; set; } = string.Empty;
}

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
