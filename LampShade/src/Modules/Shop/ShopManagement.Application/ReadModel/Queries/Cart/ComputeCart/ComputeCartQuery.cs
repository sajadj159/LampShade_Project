using MediatR;
using ShopManagement.Application.Contract.Order;
using LampShade.ReadModel.Contracts.Cart;

namespace LampShade.ReadModel.Contracts.Queries.Cart.ComputeCart;

public class ComputeCartQuery : IRequest<ShopManagement.Application.Contract.Order.Cart>
{
    public List<CartItem> CartItems { get; set; } = new();
}
