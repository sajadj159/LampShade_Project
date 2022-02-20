using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_LampShadeQuery.Contract.Product;
using _01_LampShadeQuery.Contract.ProductCategory;
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

        public ProductCategoryQuery(ShopContext context, InventoryContext inventoryContext)
        {
            _context = context;
            _inventoryContext = inventoryContext;
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

            }).ToList();
        }

        public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
        {
            var inventory = _inventoryContext.Inventory.Select(x => new {x.ProductId,x.UnitPrice,x.InStock }).ToList();
            var productCategories= _context.ProductCategories
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
                product.Price = inventory.FirstOrDefault(x => x.ProductId == product.Id)?.UnitPrice.ToMoney();
                product.InStock =  inventory.FirstOrDefault(x => x.ProductId == product.Id)?.InStock.ToString();
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