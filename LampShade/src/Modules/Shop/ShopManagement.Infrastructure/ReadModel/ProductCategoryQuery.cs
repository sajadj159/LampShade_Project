using LampShade.BuildingBlocks.Application;
using CommentManagement.Contracts;
using DiscountManagement.Contracts;
using InventoryManagement.Contracts;
using ShopManagement.Application.ReadModel.Product;
using ShopManagement.Application.ReadModel.ProductCategory;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EFCore;

namespace ShopManagement.Infrastructure.ReadModel;

public class ProductCategoryQuery(ShopContext context, IInventoryModuleApi inventoryApi, IDiscountModuleApi discountApi) : IProductCategoryQuery
{
    public async Task<ProductCategoryQueryModel> GetProductCategoryWithProductsAsync(string slug, CancellationToken cancellationToken = default)
    {
        var inventories = (await inventoryApi.GetStockAsync(null, cancellationToken)).Values.ToList();
        var discounts = await discountApi.GetActiveCustomerDiscountsAsync(cancellationToken);
        var category = await context.ProductCategories.AsNoTracking().Include(item => item.Products).FirstOrDefaultAsync(item => item.Slug == slug, cancellationToken);
        if (category is null) return new ProductCategoryQueryModel();
        var result = MapCategory(category);
        ApplyPrices(result.Products, inventories, discounts);
        return result;
    }

    public Task<List<ProductCategoryQueryModel>> GetProductCategoryQueriesAsync(CancellationToken cancellationToken = default) => context.ProductCategories.AsNoTracking().Select(item => new ProductCategoryQueryModel { Id = item.Id, Name = item.Name, PictureTitle = item.PictureTitle, PictureAlt = item.PictureAlt, PictureUrl = item.PictureUrl, Slug = item.Slug }).ToListAsync(cancellationToken);

    public async Task<List<ProductCategoryQueryModel>> GetProductCategoriesWithProductsAsync(CancellationToken cancellationToken = default)
    {
        var inventories = (await inventoryApi.GetStockAsync(null, cancellationToken)).Values.ToList();
        var discounts = await discountApi.GetActiveCustomerDiscountsAsync(cancellationToken);
        var categories = await context.ProductCategories.AsNoTracking().Include(item => item.Products).ToListAsync(cancellationToken);
        var result = categories.Select(MapCategory).ToList();
        ApplyPrices(result.SelectMany(item => item.Products), inventories, discounts);
        return result;
    }

    private static ProductCategoryQueryModel MapCategory(ShopManagement.Domain.ProductCategoryAgg.ProductCategory category) => new() { Id = category.Id, Name = category.Name, Description = category.Description, PictureUrl = category.PictureUrl, PictureAlt = category.PictureAlt, PictureTitle = category.PictureTitle, MetaDescription = category.MetaDescription, Keywords = category.Keywords, Slug = category.Slug, Products = category.Products.Select(product => MapProduct(product, category.Name)).ToList() };
    private static ProductQueryModel MapProduct(Product product, string categoryName) => new() { Id = product.Id, Category = categoryName, Name = product.Name, PictureAlt = product.PictureAlt, PictureTitle = product.PictureTitle, PictureUrl = product.PictureUrl, Slug = product.Slug };
    private static void ApplyPrices(IEnumerable<ProductQueryModel> products, IEnumerable<ProductStock> inventories, IEnumerable<ProductDiscount> discounts)
    {
        foreach (var product in products)
        {
            var inventory = inventories.FirstOrDefault(item => item.ProductId == product.Id); if (inventory is null) continue;
            product.Price = inventory.UnitPrice.ToMoney(); product.InStock = inventory.InStock;
            var discount = discounts.FirstOrDefault(item => item.ProductId == product.Id); if (discount is null) continue;
            product.DiscountRate = discount.DiscountRate; product.DiscountExpireDate = discount.EndDate.ToDiscountFormat(); product.HasDiscount = discount.DiscountRate > 0;
            product.PriceWithDiscount = (inventory.UnitPrice - Math.Round(inventory.UnitPrice * discount.DiscountRate / 100)).ToMoney();
        }
    }
}