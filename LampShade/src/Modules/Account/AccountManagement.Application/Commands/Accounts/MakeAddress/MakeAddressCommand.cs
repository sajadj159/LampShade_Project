using MediatR;
using LampShade.BuildingBlocks.Application;

namespace AccountManagement.Application.Contracts.Commands.Accounts.MakeAddress;

public class MakeAddressCommand : ICommand<OperationResult>
{
    public long AccountId { get; set; }
    public string Address { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
}
