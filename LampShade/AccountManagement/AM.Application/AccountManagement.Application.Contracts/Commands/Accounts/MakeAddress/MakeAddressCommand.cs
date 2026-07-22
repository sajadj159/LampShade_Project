using AccountManagement.Application.Contracts.AC.Account;
using MediatR;
using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.Commands.Accounts.MakeAddress;

public class MakeAddressCommand : IRequest<OperationResult>
{
    public long AccountId { get; set; }
    public string Address { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
}
