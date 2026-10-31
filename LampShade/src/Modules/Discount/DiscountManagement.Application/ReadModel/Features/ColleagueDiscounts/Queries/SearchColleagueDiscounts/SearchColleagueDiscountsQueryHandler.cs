using System.Threading;
using System.Threading.Tasks;
#nullable enable

using DiscountManagement.Application.Contract.AC.ColleagueDiscount;
using MediatR;
using DiscountManagement.Application.ReadModel.Discounts;

using DiscountManagement.Application.ReadModel.Queries.ColleagueDiscounts.SearchColleagueDiscounts;

namespace DiscountManagement.Application.ReadModel.Features.ColleagueDiscounts.Queries.SearchColleagueDiscounts;



public class SearchColleagueDiscountsQueryHandler : IRequestHandler<SearchColleagueDiscountsQuery, List<ColleagueDiscountViewModel>>
{
    private readonly IDiscountQuery _repository;

    public SearchColleagueDiscountsQueryHandler(IDiscountQuery repository)
    {
        _repository = repository;
    }

    public Task<List<ColleagueDiscountViewModel>> Handle(SearchColleagueDiscountsQuery request, CancellationToken cancellationToken)
    {
        var searchModel = new ColleagueDiscountSearchModel { ProductId = request.ProductId ?? 0 };
        return _repository.SearchColleagueDiscountsAsync(searchModel, cancellationToken);
    }
}
