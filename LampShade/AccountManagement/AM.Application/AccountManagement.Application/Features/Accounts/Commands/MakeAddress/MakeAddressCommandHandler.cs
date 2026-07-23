using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Commands.Accounts.MakeAddress;
using AccountManagement.Domain.AccountAgg;
using MediatR;

namespace AccountManagement.Application.Features.Accounts.Commands.MakeAddress;

public class MakeAddressCommandHandler(IAccountRepository accounts) : IRequestHandler<MakeAddressCommand, OperationResult>
{
    public async Task<OperationResult> Handle(MakeAddressCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult(); var account = await accounts.GetAsync(request.AccountId, cancellationToken);
        if (account is null) return operation.Failed(ApplicationMessages.RecordNotFound);
        account.MakeAddress(request.Address, request.PostalCode); return operation.Succeeded();
    }
}
