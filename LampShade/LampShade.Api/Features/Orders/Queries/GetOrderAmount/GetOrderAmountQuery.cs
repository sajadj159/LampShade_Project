using MediatR;
using ShopManagement.Application.Contract.Order;

namespace LampShade.Api.Features.Orders.Queries.GetOrderAmount;

public class GetOrderAmountQuery : IRequest<double>
{
    public long Id { get; set; }
}

public class GetOrderAmountQueryHandler : IRequestHandler<GetOrderAmountQuery, double>
{
    private readonly IOrderApplication _application;
    public GetOrderAmountQueryHandler(IOrderApplication application) => _application = application;

    public async Task<double> Handle(GetOrderAmountQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_application.GetAmountBy(request.Id));
    }
}
