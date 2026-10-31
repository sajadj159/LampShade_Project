using System.Collections.Generic;
using System.Linq;
using LampShade.BuildingBlocks.Application;
using LampShade.BuildingBlocks.Application;
using AccountManagement.Contracts;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contract;
using ShopManagement.Application.Contract.Order;
using ShopManagement.Domain.OrderAgg;

using ShopManagement.Application.ReadModel.Admin;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class OrderRepository : RepositoryBase<long, Order>, IOrderRepository, IOrderReadRepository
    {
        private readonly ShopContext _context;
        private readonly IAccountModuleApi _accounts;

        public OrderRepository(ShopContext context, IAccountModuleApi accounts):base(context)
        {
            _context = context;
            _accounts = accounts;
        }

        public double GetAmountBy(long id)
        {
            var orderPayAmount = _context.Orders.Select(x => new {x.PayAmount, x.Id}).FirstOrDefault(x => x.Id == id);
            if (orderPayAmount != null)
                return orderPayAmount.PayAmount;
            return 0;
        }

        public async Task<List<OrderViewModel>> SearchAsync(OrderSearchModel searchModel, CancellationToken cancellationToken = default)
        {
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
                PaymentProofUrl = x.PaymentProofUrl,
                CreationDate = x.CreationDate.ToFarsi()
            });
            
            queryable = queryable.Where(x => x.IsCanceled == searchModel.IsCanceled);
            
            if (searchModel.AccountId > 0)
            {
                queryable = queryable.Where(x => x.AccountId == searchModel.AccountId);
            }
            var orders = await queryable.OrderByDescending(x => x.Id).ToListAsync(cancellationToken);
            var accounts = await _accounts.GetAccountsAsync(orders.Select(x => x.AccountId), cancellationToken);

            foreach (var model in orders)
            {
                   var account = accounts.GetValueOrDefault(model.AccountId);
                   model.AccountFullName = account?.FullName;
                   model.PaymentMethodName = PaymentMethod.GetBy(model.PaymentMethodId).Name;
            }

            return orders;
        }

        public List<OrderItemViewModel> GetItemsBy(long orderId)
        {
            var products = _context.Products.Select(x=>new {x.Id,x.Name}).ToList();
            var order = _context.Orders.FirstOrDefault(x=>x.Id==orderId);
            if (order==null)
            {
                return new List<OrderItemViewModel>();
            }

            var items = order.Items.Select(x=>new OrderItemViewModel
            {
                Id = x.Id,
                ProductId = x.ProductId,
                Count = x.Count,
                UnitPrice = x.UnitPrice,
                DiscountRate = x.DiscountRate,
                OrderId = x.OrderId
            }).ToList();
            foreach (var item in items)
            {
                item.ProductName = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name;
            }
            
            return items;
        }
    }
}

