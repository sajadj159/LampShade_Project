using _0_Framework.Application;
using AccountManagement.Application.Contracts.Commands.Accounts.ChangePassword;
using AccountManagement.Domain.AccountAgg;
using MediatR;

namespace AccountManagement.Application.Features.Accounts.Commands.ChangePassword;

public class ChangePasswordCommandHandler(IAccountRepository accounts, IPasswordHasher passwordHasher) : IRequestHandler<ChangePasswordCommand, OperationResult>
{
    public Task<OperationResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        var account = accounts.Get(request.Id);
        if (account is null) return Task.FromResult(operation.Failed(ApplicationMessages.RecordNotFound));
        if (request.Password != request.RePassword) return Task.FromResult(operation.Failed(ApplicationMessages.PasswordNotMatch));
        account.ChangePassword(passwordHasher.Hash(request.Password));
        accounts.Save();
        return Task.FromResult(operation.Succeeded());
    }
}
