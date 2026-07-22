using DiscountManagement.Application.Contract.AC.CustomerDiscount;
using MediatR;
using _0_Framework.Application;

namespace DiscountManagement.Application.Contracts.Commands.CustomerDiscounts.EditCustomerDiscount;

public class EditCustomerDiscountCommand : IRequest<OperationResult>
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public int DiscountRate { get; set; }
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
}
