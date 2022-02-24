using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_LampShadeQuery.Contract.Product;
using _01_LampShadeQuery.Contract.ProductCategory;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EFCore;

namespace _01_LampShadeQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _context;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;
        public ProductCategoryQuery(ShopContext context, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _context = context;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public ProductCategoryQueryModel GetProductCategoryWithProducts(string slug)
        {
            var inventory = _inventoryContext.Inventory.Select(x => new { x.ProductId, x.UnitPrice, x.InStock }).ToList();
            var discount = _discountContext.CustomerDiscounts.Select(x => new { x.ProductId, x.DiscountRate,x.EndDate }).ToList();

            var productCategory = _context.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    MetaDescription = x.MetaDescription,
                    Keywords = x.Keywords,
                    Products = MapProducts(x.Products),
                    Slug = x.Slug
                }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);

            foreach (var product in productCategory.Products)
            {
                var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                if (productInventory == null) continue;
                {
                    var unitPrice = productInventory.UnitPrice;
                    product.Price = unitPrice.ToMoney();
                    product.InStock = productInventory.InStock; 

                    var discountRate = discount.FirstOrDefault(x => x.ProductId == product.Id);
                    if (discountRate == null) continue;
                    var rate = discountRate.DiscountRate;
                    product.DiscountExpireDate = discountRate.EndDate.ToDiscountFormat();
                    product.HasDiscount = rate > 0;
                    var discountAmount = Math.Round((unitPrice * rate) / 100);
                    product.PriceWithDiscount = (unitPrice - discountAmount).ToMoney();
                }
            }
            return productCategory;
        }

        public List<ProductCategoryQueryModel> GetProductCategoryQueries()
        {
            return _context.ProductCategories.Select(x => new ProductCategoryQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                PictureTitle = x.PictureTitle,
                PictureAlt = x.PictureAlt,
                PictureUrl = x.PictureUrl,
                Slug = x.Slug,

            }).AsNoTracking().ToList();
        }

        public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
        {
            var inventory = _inventoryContext.Inventory.Select(x => new { x.ProductId, x.UnitPrice, x.InStock }).ToList();
            var discountRate = _discountContext.CustomerDiscounts.Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now).Select(x => new { x.ProductId, x.DiscountRate }).ToList();

            var productCategories = _context.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Products = MapProducts(x.Products)
                }).ToList();

            foreach (var product in productCategories.SelectMany(x => x.Products))
            {
                var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                if (productInventory == null) continue;
                {
                    var price = productInventory.UnitPrice;
                    product.Price = price.ToMoney();
                    product.InStock = productInventory.InStock;

                    var discount = discountRate.FirstOrDefault(x => x.ProductId == product.Id);
                    if (discount == null) continue;
                    var productDiscountRate = discount.DiscountRate;
                    product.DiscountRate = productDiscountRate;
                    product.HasDiscount = productDiscountRate > 0;
                    var discountAmount = Math.Round((price * productDiscountRate) / 100);
                    product.PriceWithDiscount = (price - discountAmount).ToMoney();
                }
            }
            return productCategories;
        }

        private static List<ProductQueryModel> MapProducts(List<Product> products)
        {
            return products.Select(x => new ProductQueryModel
            {
                Id = x.Id,
                Category = x.Category.Name,
                Name = x.Name,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                PictureUrl = x.PictureUrl,
                Slug = x.Slug
            }).ToList();
        }
    }
}