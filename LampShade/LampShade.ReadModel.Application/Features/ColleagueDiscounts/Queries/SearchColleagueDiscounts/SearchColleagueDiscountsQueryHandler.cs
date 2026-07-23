using System.Threading;
using System.Threading.Tasks;
#nullable enable

using DiscountManagement.Application.Contract.AC.ColleagueDiscount;
using MediatR;
using DiscountManagement.Domain.ColleagueDiscountAgg;

using LampShade.ReadModel.Contracts.Queries.ColleagueDiscounts.SearchColleagueDiscounts;

namespace LampShade.ReadModel.Application.Features.ColleagueDiscounts.Queries.SearchColleagueDiscounts;



public class SearchColleagueDiscountsQueryHandler : IRequestHandler<SearchColleagueDiscountsQuery, List<ColleagueDiscountViewModel>>
{
    private readonly IColleagueDiscountRepository _repository;

    public SearchColleagueDiscountsQueryHandler(IColleagueDiscountRepository repository)
    {
        _repository = repository;
    }

    public Task<List<ColleagueDiscountViewModel>> Handle(SearchColleagueDiscountsQuery request, CancellationToken cancellationToken)
    {
        var searchModel = new ColleagueDiscountSearchModel { ProductId = request.ProductId ?? 0 };
        return Task.FromResult(_repository.Search(searchModel));
    }
}
