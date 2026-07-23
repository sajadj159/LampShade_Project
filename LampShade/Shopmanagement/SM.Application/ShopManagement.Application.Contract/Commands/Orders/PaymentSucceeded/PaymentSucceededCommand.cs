using _0_Framework.Application;
using MediatR;
using ShopManagement.Application.Contract.Order;

namespace ShopManagement.Application.Contracts.Commands.Orders.PaymentSucceeded;

public class PaymentSucceededCommand : ICommand<string>
{
    public long OrderId { get; set; }
    public long RefId { get; set; }
}
