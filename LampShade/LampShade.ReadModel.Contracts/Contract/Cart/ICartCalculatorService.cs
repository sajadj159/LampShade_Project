using System.Collections.Generic;
using ShopManagement.Application.Contract.Order;

namespace LampShade.ReadModel.Contracts.Cart
{
    public interface ICartCalculatorService
    {
        ShopManagement.Application.Contract.Order.Cart ComputeCart(List<CartItem> cartItems);
    }
}