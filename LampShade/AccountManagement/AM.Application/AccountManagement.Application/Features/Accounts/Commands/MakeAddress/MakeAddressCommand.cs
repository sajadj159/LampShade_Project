using AccountManagement.Application.Contracts.AC.Account;
using MediatR;
using _0_Framework.Application;

namespace AccountManagement.Application.Features.Accounts.Commands.MakeAddress;

public class MakeAddressCommand : IRequest<OperationResult>
{
    public long AccountId { get; set; }
    public string Address { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
}

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
