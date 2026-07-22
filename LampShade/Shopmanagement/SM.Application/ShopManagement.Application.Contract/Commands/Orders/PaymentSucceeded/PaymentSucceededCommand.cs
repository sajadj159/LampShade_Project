using MediatR;
using ShopManagement.Application.Contract.Order;

namespace ShopManagement.Application.Contracts.Commands.Orders.PaymentSucceeded;

public class PaymentSucceededCommand : IRequest<string>
{
    public long OrderId { get; set; }
    public long RefId { get; set; }
}
