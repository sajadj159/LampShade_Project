using DiscountManagement.Application.Contract.AC.CustomerDiscount;
using MediatR;
using DiscountManagement.Domain.CustomerDiscountAgg;

using LampShade.ReadModel.Contracts.Queries.CustomerDiscounts.GetCustomerDiscountById;

namespace LampShade.ReadModel.Application.Features.CustomerDiscounts.Queries.GetCustomerDiscountById;



public class GetCustomerDiscountByIdQueryHandler : IRequestHandler<GetCustomerDiscountByIdQuery, EditCustomerDiscount>
{
    private readonly ICustomerDiscountRepository _repository;

    public GetCustomerDiscountByIdQueryHandler(ICustomerDiscountRepository repository)
    {
        _repository = repository;
    }

    public Task<EditCustomerDiscount> Handle(GetCustomerDiscountByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_repository.GetDetails(request.Id));
    }
}

