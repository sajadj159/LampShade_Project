#nullable enable

using DiscountManagement.Application.Contract.AC.ColleagueDiscount;
using MediatR;

namespace LampShade.ReadModel.Contracts.Queries.ColleagueDiscounts.SearchColleagueDiscounts;

public class SearchColleagueDiscountsQuery : IRequest<List<ColleagueDiscountViewModel>>
{
    public long? ProductId { get; set; }
}
