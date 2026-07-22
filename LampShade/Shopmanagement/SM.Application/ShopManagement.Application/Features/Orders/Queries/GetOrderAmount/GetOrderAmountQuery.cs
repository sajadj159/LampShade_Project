using MediatR;
using ShopManagement.Application.Contract.Order;

namespace ShopManagement.Application.Features.Orders.Queries.GetOrderAmount;

public class GetOrderAmountQuery : IRequest<double>
{
    public long Id { get; set; }
}

public class GetOrderAmountQueryHandler : IRequestHandler<GetOrderAmountQuery, double>
{
    private readonly IOrderApplication _application;
    public GetOrderAmountQueryHandler(IOrderApplication application) => _application = application;

    public Task<double> Handle(GetOrderAmountQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_application.GetAmountBy(request.Id));
    }
}
