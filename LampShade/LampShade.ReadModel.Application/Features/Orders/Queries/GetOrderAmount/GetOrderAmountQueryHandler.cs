using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShopManagement.Application.Contract.Order;

using LampShade.ReadModel.Contracts.Queries.Orders.GetOrderAmount;

namespace LampShade.ReadModel.Application.Features.Orders.Queries.GetOrderAmount;



public class GetOrderAmountQueryHandler : IRequestHandler<GetOrderAmountQuery, double>
{
    private readonly IOrderApplication _application;
    public GetOrderAmountQueryHandler(IOrderApplication application) => _application = application;

    public Task<double> Handle(GetOrderAmountQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_application.GetAmountBy(request.Id));
    }
}
