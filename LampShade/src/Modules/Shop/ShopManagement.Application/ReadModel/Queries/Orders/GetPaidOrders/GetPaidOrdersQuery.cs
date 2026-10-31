using MediatR;
using ShopManagement.Application.ReadModel.Order;

namespace ShopManagement.Application.ReadModel.Queries.Orders.GetPaidOrders;

public class GetPaidOrdersQuery : IRequest<List<OrderQueryModel>> { }
