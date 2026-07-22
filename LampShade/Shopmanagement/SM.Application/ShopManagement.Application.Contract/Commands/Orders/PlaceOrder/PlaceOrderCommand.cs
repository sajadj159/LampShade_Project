using MediatR;
using ShopManagement.Application.Contract.Order;

namespace ShopManagement.Application.Contracts.Commands.Orders.PlaceOrder;

public class PlaceOrderCommand : IRequest<long>
{
    public double TotalAmount { get; set; }
    public double DiscountAmount { get; set; }
    public double PayAmount { get; set; }
    public int PaymentMethod { get; set; }
    public List<CartItem> Items { get; set; } = new();
}
