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

        public List<OrderQueryModel> GetOrders(long accountId)
        {
            return  _shopContext.Orders.Select(x=>new OrderQueryModel
            {
                Id = x.Id,
                PaymentMethodId = x.PaymentMethod,
                PayAmount = x.PayAmount,
                IsPaid = x.IsPaid,
                AccountId = x.AccountId,
                TotalAmount = x.TotalAmount,
                DiscountAmount = x.DiscountAmount,
                IssueTrackingNo = x.IssueTrackingNumber,
                PayDate = x.CreationDate.ToFarsi()
            }).Where(x=>x.AccountId==accountId).ToList();
        }
    }
}