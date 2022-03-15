using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_LampShadeQuery.Contract.Order;
using ShopManagement.Infrastructure.EFCore;

namespace _01_LampShadeQuery.Query
{
    public class OrderQuery:IOrderQuery
    {
        private readonly ShopContext _shopContext;

        public OrderQuery(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public List<OrderQueryModel> GetPayedOrders()
        {

            return _shopContext.Orders.Where(x => x.IsPaid).Select(x => new OrderQueryModel
            {
                Id = x.Id,
                PaymentMethodId = x.PaymentMethod,
                PayAmount = x.PayAmount,
            }).ToList();
        }

    }
}