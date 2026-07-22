#nullable enable

using DiscountManagement.Application.Contract.AC.CustomerDiscount;
using MediatR;

using LampShade.ReadModel.Contracts.Queries.CustomerDiscounts.SearchCustomerDiscounts;

namespace LampShade.ReadModel.Application.Features.CustomerDiscounts.Queries.SearchCustomerDiscounts;



public class SearchCustomerDiscountsQueryHandler : IRequestHandler<SearchCustomerDiscountsQuery, List<CustomerDiscountViewmodel>>
{
    private readonly ICustomerDiscountApplication _application;

    public SearchCustomerDiscountsQueryHandler(ICustomerDiscountApplication application)
    {
        _application = application;
    }

    public Task<List<CustomerDiscountViewmodel>> Handle(SearchCustomerDiscountsQuery request, CancellationToken cancellationToken)
    {
        var searchModel = new CostumerDiscountSearchModel
        {
            ProductId = request.ProductId ?? 0,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };
        return Task.FromResult(_application.Search(searchModel));
    }
}

