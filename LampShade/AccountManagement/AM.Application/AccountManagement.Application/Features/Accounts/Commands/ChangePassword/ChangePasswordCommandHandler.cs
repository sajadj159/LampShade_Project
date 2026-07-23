using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Commands.Accounts.ChangePassword;
using AccountManagement.Domain.AccountAgg;
using MediatR;

namespace AccountManagement.Application.Features.Accounts.Commands.ChangePassword;

public class ChangePasswordCommandHandler(IAccountRepository accounts, IPasswordHasher passwordHasher) : IRequestHandler<ChangePasswordCommand, OperationResult>
{
    public async Task<OperationResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult(); var account = await accounts.GetAsync(request.Id, cancellationToken);
        if (account is null) return operation.Failed(ApplicationMessages.RecordNotFound);
        if (request.Password != request.RePassword) return operation.Failed(ApplicationMessages.PasswordNotMatch);
        account.ChangePassword(passwordHasher.Hash(request.Password)); return operation.Succeeded();
    }
}
