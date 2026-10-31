using ShopManagement.Application.Contract.Order;

namespace ShopManagement.Application.Cart
{
    public class CartService:ICartService
    {
        public Contract.Order.Cart Cart { get; set; }
        public void Set(Contract.Order.Cart cart)
        {
            Cart = cart;
        }

        public Contract.Order.Cart Get()
        {
            return Cart;
        }
    }
}