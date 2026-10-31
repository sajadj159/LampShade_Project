using System.Threading;
using System.Threading.Tasks;
#nullable enable

using DiscountManagement.Application.Contract.AC.CustomerDiscount;
using MediatR;
using DiscountManagement.Application.ReadModel.Discounts;

using DiscountManagement.Application.ReadModel.Queries.CustomerDiscounts.SearchCustomerDiscounts;

namespace DiscountManagement.Application.ReadModel.Features.CustomerDiscounts.Queries.SearchCustomerDiscounts;



public class SearchCustomerDiscountsQueryHandler : IRequestHandler<SearchCustomerDiscountsQuery, List<CustomerDiscountViewmodel>>
{
    private readonly IDiscountQuery _repository;

    public SearchCustomerDiscountsQueryHandler(IDiscountQuery repository)
    {
        _repository = repository;
    }

    public Task<List<CustomerDiscountViewmodel>> Handle(SearchCustomerDiscountsQuery request, CancellationToken cancellationToken)
    {
        var searchModel = new CostumerDiscountSearchModel
        {
            ProductId = request.ProductId ?? 0,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };
        return _repository.SearchCustomerDiscountsAsync(searchModel, cancellationToken);
    }
}
