using MediatR;
using LampShade.ReadModel.Contracts.Order;

namespace LampShade.ReadModel.Contracts.Queries.Orders.GetPaidOrders;

public class GetPaidOrdersQuery : IRequest<List<OrderQueryModel>> { }
