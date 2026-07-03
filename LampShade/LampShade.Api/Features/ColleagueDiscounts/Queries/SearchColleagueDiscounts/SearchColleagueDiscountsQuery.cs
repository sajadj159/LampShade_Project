using DiscountManagement.Application.Contract.AC.ColleagueDiscount;
using MediatR;

namespace LampShade.Api.Features.ColleagueDiscounts.Queries.SearchColleagueDiscounts;

public class SearchColleagueDiscountsQuery : IRequest<List<ColleagueDiscountViewModel>>
{
    public long? ProductId { get; set; }
}

public class SearchColleagueDiscountsQueryHandler : IRequestHandler<SearchColleagueDiscountsQuery, List<ColleagueDiscountViewModel>>
{
    private readonly IColleagueDiscountApplication _application;

    public SearchColleagueDiscountsQueryHandler(IColleagueDiscountApplication application)
    {
        _application = application;
    }

    public async Task<List<ColleagueDiscountViewModel>> Handle(SearchColleagueDiscountsQuery request, CancellationToken cancellationToken)
    {
        var searchModel = new ColleagueDiscountSearchModel { ProductId = request.ProductId ?? 0 };
        return await Task.FromResult(_application.Search(searchModel));
    }
}

