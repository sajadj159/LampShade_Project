using DiscountManagement.Application.Contract.AC.CustomerDiscount;
using MediatR;

namespace DiscountManagement.Application.Features.CustomerDiscounts.Queries.GetCustomerDiscountById;

public class GetCustomerDiscountByIdQuery : IRequest<EditCustomerDiscount>
{
    public long Id { get; set; }
}

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
