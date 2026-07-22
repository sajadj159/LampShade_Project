using DiscountManagement.Application.Contract.AC.CustomerDiscount;
using MediatR;

using LampShade.ReadModel.Contracts.Queries.CustomerDiscounts.GetCustomerDiscountById;

namespace LampShade.ReadModel.Application.Features.CustomerDiscounts.Queries.GetCustomerDiscountById;



public class GetCustomerDiscountByIdQueryHandler : IRequestHandler<GetCustomerDiscountByIdQuery, EditCustomerDiscount>
{
    private readonly ICustomerDiscountApplication _application;

    public GetCustomerDiscountByIdQueryHandler(ICustomerDiscountApplication application)
    {
        _application = application;
    }

    public Task<EditCustomerDiscount> Handle(GetCustomerDiscountByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_application.GetDetails(request.Id));
    }
}
