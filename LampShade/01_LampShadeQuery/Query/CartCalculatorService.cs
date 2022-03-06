﻿using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Repository;
using _01_LampShadeQuery.Contract.Cart;
using DiscountManagement.Infrastructure.EFCore;
using ShopManagement.Application.Contract.Order;

namespace _01_LampShadeQuery.Query
{
    public class CartCalculatorService : ICartCalculatorService
    {
        private readonly IAuthHelper _authHelper;
        private readonly DiscountContext _discountContext;

        public CartCalculatorService(DiscountContext discountContext, IAuthHelper authHelper)
        {
            _discountContext = discountContext;
            _authHelper = authHelper;
        }

        public Cart ComputeCart(List<CartItem> cartItems)
        {
            var cart = new Cart();
            var colleagueDiscounts = _discountContext.ColleagueDiscounts
                .Where(x => !x.IsRemoved)
                .Select(x => new { x.DiscountRate, x.ProductId }).ToList();
            var customerDiscounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate }).ToList();

            var currentAccountRole = _authHelper.CurrentAccountRole();
            foreach (var cartItem in cartItems)
            {
                if (currentAccountRole == Roles.ColleagueUser)
                {
                    var colleagueDiscount = colleagueDiscounts.FirstOrDefault(x => x.ProductId == cartItem.Id);
                    if (colleagueDiscount == null) continue;
                    cartItem.DiscountRate=colleagueDiscount.DiscountRate;
                }
                else
                {
                    var customerDiscount = customerDiscounts.FirstOrDefault(x => x.ProductId == cartItem.Id);
                    if (customerDiscount == null) continue;
                    cartItem.DiscountRate=customerDiscount.DiscountRate;
                }
                cartItem.DiscountAmount = ((cartItem.TotalItemPrice * cartItem.DiscountRate) / 100);
                cartItem.ItemPayAmount = cartItem.TotalItemPrice - cartItem.DiscountAmount;
                cart.Add(cartItem);
            }
            return cart;
        }
    }
}