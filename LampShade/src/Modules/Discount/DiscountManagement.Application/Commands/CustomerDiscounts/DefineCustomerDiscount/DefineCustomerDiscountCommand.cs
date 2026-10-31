using DiscountManagement.Application.Contract.AC.CustomerDiscount;
using MediatR;
using LampShade.BuildingBlocks.Application;

namespace DiscountManagement.Application.Contracts.Commands.CustomerDiscounts.DefineCustomerDiscount;

public class DefineCustomerDiscountCommand : ICommand<OperationResult>
{
    public long ProductId { get; set; }
    public int DiscountRate { get; set; }
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
}
