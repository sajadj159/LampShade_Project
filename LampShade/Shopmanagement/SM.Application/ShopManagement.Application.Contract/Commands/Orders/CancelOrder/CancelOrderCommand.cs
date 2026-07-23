using MediatR;
using ShopManagement.Application.Contract.Order;

namespace ShopManagement.Application.Contracts.Commands.Orders.CancelOrder;

public class CancelOrderCommand : IRequest
{
    public long Id { get; set; }
}
