using System.Threading;
using System.Threading.Tasks;
using DiscountManagement.Application.Contract.AC.CustomerDiscount;
using MediatR;
using DiscountManagement.Application.ReadModel.Discounts;

using DiscountManagement.Application.ReadModel.Queries.CustomerDiscounts.GetCustomerDiscountById;

namespace DiscountManagement.Application.ReadModel.Features.CustomerDiscounts.Queries.GetCustomerDiscountById;



public class GetCustomerDiscountByIdQueryHandler : IRequestHandler<GetCustomerDiscountByIdQuery, EditCustomerDiscount>
{
    private readonly IDiscountQuery _repository;

    public GetCustomerDiscountByIdQueryHandler(IDiscountQuery repository)
    {
        _repository = repository;
    }

    public Task<EditCustomerDiscount> Handle(GetCustomerDiscountByIdQuery request, CancellationToken cancellationToken)
    {
        return _repository.GetCustomerDiscountDetailsAsync(request.Id, cancellationToken);
    }
}
