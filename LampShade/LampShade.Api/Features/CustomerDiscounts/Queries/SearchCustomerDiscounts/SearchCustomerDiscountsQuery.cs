using DiscountManagement.Application.Contract.AC.CustomerDiscount;
using MediatR;

namespace LampShade.Api.Features.CustomerDiscounts.Queries.SearchCustomerDiscounts;

public class SearchCustomerDiscountsQuery : IRequest<List<CustomerDiscountViewmodel>>
{
    public long? ProductId { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
}

public class SearchCustomerDiscountsQueryHandler : IRequestHandler<SearchCustomerDiscountsQuery, List<CustomerDiscountViewmodel>>
{
    private readonly ICustomerDiscountApplication _application;

    public SearchCustomerDiscountsQueryHandler(ICustomerDiscountApplication application)
    {
        _application = application;
    }

    public async Task<List<CustomerDiscountViewmodel>> Handle(SearchCustomerDiscountsQuery request, CancellationToken cancellationToken)
    {
        var searchModel = new CostumerDiscountSearchModel
        {
            ProductId = request.ProductId ?? 0,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };
        return await Task.FromResult(_application.Search(searchModel));
    }
}

