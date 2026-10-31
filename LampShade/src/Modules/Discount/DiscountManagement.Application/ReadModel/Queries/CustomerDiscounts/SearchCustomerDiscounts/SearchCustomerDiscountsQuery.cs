#nullable enable

using DiscountManagement.Application.Contract.AC.CustomerDiscount;
using MediatR;

namespace LampShade.ReadModel.Contracts.Queries.CustomerDiscounts.SearchCustomerDiscounts;

public class SearchCustomerDiscountsQuery : IRequest<List<CustomerDiscountViewmodel>>
{
    public long? ProductId { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
}
