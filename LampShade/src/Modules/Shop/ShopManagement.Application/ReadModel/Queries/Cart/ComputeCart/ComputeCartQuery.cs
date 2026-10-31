using MediatR;
using ShopManagement.Application.Contract.Order;

namespace ShopManagement.Application.ReadModel.Queries.Cart.ComputeCart;

public class ComputeCartQuery : IRequest<ShopManagement.Application.Contract.Order.Cart>
{
    public List<CartItem> CartItems { get; set; } = new();
}
