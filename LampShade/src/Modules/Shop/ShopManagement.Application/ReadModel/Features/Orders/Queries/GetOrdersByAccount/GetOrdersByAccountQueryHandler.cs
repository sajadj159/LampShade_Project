using MediatR;
using ShopManagement.Application.Orders;
using ShopManagement.Application.ReadModel.Order;
using ShopManagement.Application.ReadModel.Queries.Orders.GetOrdersByAccount;

namespace ShopManagement.Application.ReadModel.Features.Orders.Queries.GetOrdersByAccount;

public class GetOrdersByAccountQueryHandler(IOrderQuery orderQuery, OrderAccessPolicy access) : IRequestHandler<GetOrdersByAccountQuery, List<OrderQueryModel>>
{
    // Customers only ever see their own orders, whatever account id the URL carries.
    public Task<List<OrderQueryModel>> Handle(GetOrdersByAccountQuery request, CancellationToken cancellationToken) =>
        orderQuery.GetOrdersAsync(access.IsAdministrator ? request.AccountId : access.CurrentAccountId, cancellationToken);
}
