using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_LampShadeQuery.Contract.Product;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;

namespace _01_LampShadeQuery.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;

        public ProductQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public List<ProductQueryModel> Search(string value)
        {
            var inventory = _inventoryContext.Inventory.Select(x => new { x.ProductId, x.InStock, x.UnitPrice }).ToList();
            var discounts = _discountContext.CustomerDiscounts.Select(x => new { x.ProductId, x.DiscountRate, x.EndDate }).ToList();
            var queryable = _shopContext.Products.Include(x => x.Category).Select(x => new ProductQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Category = x.Category.Name,
                CategorySlug = x.Category.Slug,
                PictureUrl = x.PictureUrl,
                PictureTitle = x.PictureTitle,
                PictureAlt = x.PictureAlt,
                ShortDescription = x.ShortDescription,
                Slug = x.Slug
            }).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(value))
            {
                queryable = queryable.Where(x => x.Name.Contains(value) || x.ShortDescription.Contains(value));
            }

            var products = queryable.OrderByDescending(x => x.Id).ToList();

            foreach (var product in products)
            {
                var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                if (productInventory == null) continue;
                {
                    var price = productInventory.UnitPrice;
                    product.Price = price.ToMoney();
                    product.InStock = inventory.FirstOrDefault(x => x.ProductId == product.Id)?.InStock.ToString();

                    var productDiscount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                    if (productDiscount == null) continue;
                    var discountRate = productDiscount.DiscountRate;
                    product.DiscountExpireDate = productDiscount.EndDate.ToDiscountFormat();
                    product.HasDiscount = discountRate > 0;
                    product.DiscountRate = discountRate;
                    var discountAmount = Math.Round((price * discountRate) / 100);
                    product.PriceWithDiscount = (price - discountAmount).ToMoney();
                }
            }
            return products;
        }
    }
}