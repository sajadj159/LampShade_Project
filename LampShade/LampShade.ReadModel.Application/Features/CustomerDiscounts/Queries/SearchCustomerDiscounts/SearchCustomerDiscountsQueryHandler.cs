#nullable enable

using DiscountManagement.Application.Contract.AC.CustomerDiscount;
using MediatR;
using DiscountManagement.Domain.CustomerDiscountAgg;

using LampShade.ReadModel.Contracts.Queries.CustomerDiscounts.SearchCustomerDiscounts;

namespace LampShade.ReadModel.Application.Features.CustomerDiscounts.Queries.SearchCustomerDiscounts;



public class SearchCustomerDiscountsQueryHandler : IRequestHandler<SearchCustomerDiscountsQuery, List<CustomerDiscountViewmodel>>
{
    private readonly ICustomerDiscountRepository _repository;

    public SearchCustomerDiscountsQueryHandler(ICustomerDiscountRepository repository)
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
        return Task.FromResult(_repository.Search(searchModel));
    }
}


