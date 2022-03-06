using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using _0_Framework.Repository;
using _01_LampShadeQuery.Contract.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contract.Order;

namespace ServiceHost.Pages
{
    public class CartModel : PageModel
    {
        public List<CartItem> CartItems;
        public const string CookieName = "cart-items";
        private readonly IProductQuery _productQuery;

        public CartModel(IProductQuery productQuery)
        {
            CartItems = new List<CartItem>();
            _productQuery = productQuery;
        }

        public void OnGet()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            if (value == null)
                return;
            var cartItems = serializer.Deserialize<List<CartItem>>(value);

            foreach (var item in cartItems)
            {
                item.TotalItemPrice = item.UnitPrice * item.Count;
            }

            CartItems = _productQuery.CheckInventoryStatus(cartItems);
        }

        public RedirectToPageResult OnGetRemoveFromCart(long id)
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            var itemToRemove = cartItems.FirstOrDefault(x => x.Id == id);
            cartItems.Remove(itemToRemove);
            var options = new CookieOptions {Expires = DateTime.Now.AddDays(2), Path = "/"};
            var serializeValue = serializer.Serialize(cartItems);
            //Response.Cookies.Delete(CookieName);
            HttpContext.Response.Cookies.Append("user_id", "1");

            //Response.Cookies.Append(CookieName, serializeValue, options);
            //return RedirectToAction("Get"); 
            return RedirectToPage("/Cart");
        }

        public RedirectToPageResult OnGetGoToCheckOut()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            if (value == null)
                return null;
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            foreach (var item in cartItems)
            {
                item.TotalItemPrice = item.UnitPrice * item.Count;
            }

            CartItems = _productQuery.CheckInventoryStatus(cartItems);
            if (cartItems.Any(x=>!x.IsInStock))
            {
                return RedirectToPage("/Cart");
            }

            return RedirectToPage("CheckOut");
        }
    }
}