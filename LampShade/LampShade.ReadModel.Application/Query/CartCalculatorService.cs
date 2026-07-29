using _0_Framework.Application;
using _0_Framework.Repository;
using LampShade.ReadModel.Contracts.Acl;
using LampShade.ReadModel.Contracts.Cart;
using ShopManagement.Application.Contract.Order;

namespace LampShade.ReadModel.Application.Query;

public class CartCalculatorService(IShopDiscountReadAcl discountAcl, IAuthHelper authHelper) : ICartCalculatorService
{
    public Cart ComputeCart(List<CartItem> cartItems)
    {
        var cart = new Cart();
        var colleagueDiscounts = discountAcl.GetColleagueDiscounts();
        var customerDiscounts = discountAcl.GetCustomerDiscounts(true);
        var currentAccountRole = authHelper.CurrentAccountRole();
        foreach (var cartItem in cartItems)
        {
            cartItem.CalculateTotalItemPrice();
            var discount = currentAccountRole == Roles.ColleagueUser ? colleagueDiscounts.FirstOrDefault(item => item.ProductId == cartItem.Id) : customerDiscounts.FirstOrDefault(item => item.ProductId == cartItem.Id);
            if (discount is not null) cartItem.DiscountRate = discount.DiscountRate;
            cartItem.DiscountAmount = cartItem.TotalItemPrice * cartItem.DiscountRate / 100;
            cartItem.ItemPayAmount = cartItem.TotalItemPrice - cartItem.DiscountAmount;
            cart.Add(cartItem);
        }
        return cart;
    }
}