using System.Collections.Generic;
using ShopManagement.Application.Contract.Order;

namespace _01_LampShadeQuery.Contract.Cart
{
    public interface ICartCalculatorService
    {
        ShopManagement.Application.Contract.Order.Cart ComputeCart(List<CartItem> cartItems);
    }
}