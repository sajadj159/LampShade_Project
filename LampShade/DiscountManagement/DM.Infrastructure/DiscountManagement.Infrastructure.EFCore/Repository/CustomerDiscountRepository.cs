using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Repository;
using DiscountManagement.Application.Contract.AC.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using ShopManagement.Infrastructure.EFCore;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class CustomerDiscountRepository : RepositoryBase<long, CustomerDiscount>, ICustomerDiscountRepository
    {
        private readonly DiscountContext _context;
        private readonly ShopContext _shopContext;
        public CustomerDiscountRepository(DiscountContext context, ShopContext shopContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _context.CustomerDiscounts
                .Select(x => new EditCustomerDiscount
                {
                    Id = x.Id,
                    DiscountRate = x.DiscountRate,
                    ProductId = x.ProductId,
                    Reason = x.Reason,
                    EndDate = x.EndDate.ToString(CultureInfo.InvariantCulture),
                    StartDate = x.StartDate.ToString(CultureInfo.InvariantCulture)
                })
                .FirstOrDefault(x => x.Id == id);
        }

        public List<CustomerDiscountViewmodel> Search(CustomerDiscountSearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var queryable = _context.CustomerDiscounts
                .Select(x => new CustomerDiscountViewmodel
                {
                    Id = x.Id,
                    DiscountRate = x.DiscountRate,
                    ProductId = x.ProductId,
                    Reason = x.Reason,
                    StartDate = x.StartDate.ToFarsi(),
                    EndDate = x.EndDate.ToFarsi(),
                    StartDateGr = x.StartDate,
                    EndDateGr = x.EndDate,
                    CreationDate = x.CreationDate.ToFarsi()
                });
            if (searchModel.ProductId > 0)
            {
                queryable = queryable.Where(x => x.ProductId == searchModel.ProductId);
            }

            if (!string.IsNullOrWhiteSpace(searchModel.StartDate))
            {
                queryable = queryable.Where(x => x.StartDateGr >= searchModel.StartDate.ToGeorgianDateTime());
            }

            if (!string.IsNullOrWhiteSpace(searchModel.EndDate))
            {
                queryable = queryable.Where(x => x.EndDateGr <= searchModel.EndDate.ToGeorgianDateTime());
            }

            var discounts = queryable.OrderByDescending(x => x.Id).ToList();
            discounts.ForEach(discount => discount.Product = products.FirstOrDefault(x => x.Id == discount.ProductId)?.Name);
            return discounts;
        }
    }
}