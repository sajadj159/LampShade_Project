using AccountManagement.Application.Contracts.AC.Account;
using MediatR;
using _0_Framework.Application;

using AccountManagement.Application.Contracts.Commands.Accounts.MakeAddress;

namespace AccountManagement.Application.Features.Accounts.Commands.MakeAddress;



public class MakeAddressCommandHandler : IRequestHandler<MakeAddressCommand, OperationResult>
{
    private readonly IAccountApplication _accountApplication;

    public MakeAddressCommandHandler(IAccountApplication accountApplication)
    {
        _accountApplication = accountApplication;
    }

    public Task<OperationResult> Handle(MakeAddressCommand request, CancellationToken cancellationToken)
    {
        var command = new AccountManagement.Application.Contracts.AC.Account.MakeAddress
        {
            AccountId = request.AccountId,
            Address = request.Address,
            PostalCode = request.PostalCode
        };
        return Task.FromResult(_accountApplication.MakeAddress(command));
    }
}
