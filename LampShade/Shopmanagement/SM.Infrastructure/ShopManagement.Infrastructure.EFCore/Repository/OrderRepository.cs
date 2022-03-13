using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Repository;
using AccountManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contract;
using ShopManagement.Application.Contract.Order;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class OrderRepository : RepositoryBase<long, Order>, IOrderRepository
    {
        private readonly ShopContext _context;
        private readonly AccountContext _accountContext;

        public OrderRepository(ShopContext context, AccountContext accountContext):base(context)
        {
            _context = context;
            _accountContext = accountContext;
        }

        public double GetAmountBy(long id)
        {
            var orderPayAmount = _context.Orders.Select(x => new {x.PayAmount, x.Id}).FirstOrDefault(x => x.Id == id);
            if (orderPayAmount != null)
                return orderPayAmount.PayAmount;
            return 0;
        }

        public List<OrderViewModel> Search(OrderSearchModel searchModel)
        {
            var accounts = _accountContext.Accounts.Select(x=>new {x.FullName,x.Id}).ToList();
            var queryable = _context.Orders
                .Select(x => new OrderViewModel
            {
                Id = x.Id,
                AccountId = x.AccountId,
                TotalAmount = x.TotalAmount,
                PaymentMethodId = x.PaymentMethod,
                DiscountAmount = x.DiscountAmount,
                PayAmount = x.PayAmount,
                IsPaid = x.IsPaid,
                IsCanceled = x.IsCanceled,
                IssueTrackingNumber = x.IssueTrackingNumber,
                RefId = x.RefId,
                CreationDate = x.CreationDate.ToFarsi()
            });
            
            queryable = queryable.Where(x => x.IsCanceled == searchModel.IsCanceled);
            
            if (searchModel.AccountId > 0)
            {
                queryable = queryable.Where(x => x.AccountId == searchModel.AccountId);
            }
            var orders= queryable.OrderByDescending(x=>x.Id).ToList();

            foreach (var model in orders)
            {
                   var account = accounts.FirstOrDefault(x => x.Id == model.AccountId);
                   model.AccountFullName = account?.FullName;
                   model.PaymentMethodName = PaymentMethod.GetBy(model.PaymentMethodId).Name;
            }

            return orders;
        }
    }
}