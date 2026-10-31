using DiscountManagement.Application.Contract.AC.CustomerDiscount;
using MediatR;

namespace DiscountManagement.Application.ReadModel.Queries.CustomerDiscounts.GetCustomerDiscountById;

public class GetCustomerDiscountByIdQuery : IRequest<EditCustomerDiscount>
{
    public long Id { get; set; }
}
