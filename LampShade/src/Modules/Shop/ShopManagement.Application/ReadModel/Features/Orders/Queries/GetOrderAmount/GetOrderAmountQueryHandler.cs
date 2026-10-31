using MediatR;
using ShopManagement.Application.Orders;
using ShopManagement.Application.ReadModel.Admin;
using ShopManagement.Application.ReadModel.Queries.Orders.GetOrderAmount;

namespace ShopManagement.Application.ReadModel.Features.Orders.Queries.GetOrderAmount;

public class GetOrderAmountQueryHandler(IOrderReadRepository orders, OrderAccessPolicy access) : IRequestHandler<GetOrderAmountQuery, double>
{
    public async Task<double> Handle(GetOrderAmountQuery request, CancellationToken cancellationToken) =>
        await access.CanAccessAsync(request.Id, cancellationToken) ? orders.GetAmountBy(request.Id) : 0;
}
