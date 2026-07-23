using DiscountManagement.Application.Contract.AC.CustomerDiscount;
using MediatR;

namespace LampShade.ReadModel.Contracts.Queries.CustomerDiscounts.GetCustomerDiscountById;

public class GetCustomerDiscountByIdQuery : IRequest<EditCustomerDiscount>
{
    public long Id { get; set; }
}
