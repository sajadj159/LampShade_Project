using _0_Framework.Application;
using AccountManagement.Application.Contracts.Commands.Accounts.MakeAddress;
using AccountManagement.Domain.AccountAgg;
using MediatR;

namespace AccountManagement.Application.Features.Accounts.Commands.MakeAddress;

public class MakeAddressCommandHandler(IAccountRepository accounts) : IRequestHandler<MakeAddressCommand, OperationResult>
{
    public Task<OperationResult> Handle(MakeAddressCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        var account = accounts.Get(request.AccountId);
        if (account is null) return Task.FromResult(operation.Failed(ApplicationMessages.RecordNotFound));
        account.MakeAddress(request.Address, request.PostalCode);
        accounts.Save();
        return Task.FromResult(operation.Succeeded());
    }
}
