using System.Collections.Generic;
using _0_Framework.Application;
using _01_LampShadeQuery.Contract.Cart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contract.Order;

namespace ServiceHost.Pages
{
    public class CheckoutModel : PageModel
    {
        public const string CookieName = "cart-items";
        public Cart Cart;

        private readonly ICartCalculatorService _cartCalculatorService;
        private readonly IAuthHelper _authHelper;

        public CheckoutModel(ICartCalculatorService cartCalculatorService, IAuthHelper authHelper)
        {
            _cartCalculatorService = cartCalculatorService;
            _authHelper = authHelper;
        }

        public RedirectToPageResult OnGet()
        {
            if (!_authHelper.IsAuthenticated())
            {
               return RedirectToPage("/Account");
               
            }

            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            if (value == null)
                return null;
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            foreach (var item in cartItems)
            {
                item.CalculateTotalItemPrice();
            }

            Cart = _cartCalculatorService.ComputeCart(cartItems);
            return null;
        }
    }
}
