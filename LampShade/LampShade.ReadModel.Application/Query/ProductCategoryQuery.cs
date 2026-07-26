using _0_Framework.Application;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using LampShade.ReadModel.Contracts.Product;
using LampShade.ReadModel.Contracts.ProductCategory;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EFCore;

namespace LampShade.ReadModel.Application.Query;

public class ProductCategoryQuery(ShopContext context, InventoryContext inventoryContext, DiscountContext discountContext) : IProductCategoryQuery
{
    public async Task<ProductCategoryQueryModel> GetProductCategoryWithProductsAsync(string slug, CancellationToken cancellationToken = default)
    {
        var inventories = await inventoryContext.Inventory.AsNoTracking().Select(x => new InventorySnapshot(x.ProductId, x.UnitPrice, x.InStock)).ToListAsync(cancellationToken);
        var discounts = await discountContext.CustomerDiscounts.AsNoTracking().Select(x => new DiscountSnapshot(x.ProductId, x.DiscountRate, x.EndDate)).ToListAsync(cancellationToken);
        var category = await context.ProductCategories.AsNoTracking().Include(x => x.Products).ThenInclude(x => x.Category).FirstOrDefaultAsync(x => x.Slug == slug, cancellationToken);
        if (category is null) return new ProductCategoryQueryModel();
        var result = MapCategory(category);
        ApplyPrices(result.Products, inventories, discounts);
        return result;
    }

    public Task<List<ProductCategoryQueryModel>> GetProductCategoryQueriesAsync(CancellationToken cancellationToken = default) =>
        context.ProductCategories.AsNoTracking().Select(x => new ProductCategoryQueryModel { Id = x.Id, Name = x.Name, PictureTitle = x.PictureTitle, PictureAlt = x.PictureAlt, PictureUrl = x.PictureUrl, Slug = x.Slug }).ToListAsync(cancellationToken);

    public async Task<List<ProductCategoryQueryModel>> GetProductCategoriesWithProductsAsync(CancellationToken cancellationToken = default)
    {
        var inventories = await inventoryContext.Inventory.AsNoTracking().Select(x => new InventorySnapshot(x.ProductId, x.UnitPrice, x.InStock)).ToListAsync(cancellationToken);
        var discounts = await discountContext.CustomerDiscounts.AsNoTracking().Where(x => x.StartDate < DateTime.UtcNow && x.EndDate > DateTime.UtcNow).Select(x => new DiscountSnapshot(x.ProductId, x.DiscountRate, x.EndDate)).ToListAsync(cancellationToken);
        var categories = await context.ProductCategories.AsNoTracking().Include(x => x.Products).ThenInclude(x => x.Category).ToListAsync(cancellationToken);
        var result = categories.Select(MapCategory).ToList();
        ApplyPrices(result.SelectMany(x => x.Products), inventories, discounts);
        return result;
    }

    private sealed record InventorySnapshot(long ProductId, double UnitPrice, bool InStock);
    private sealed record DiscountSnapshot(long ProductId, int DiscountRate, DateTime EndDate);

    private static ProductCategoryQueryModel MapCategory(ShopManagement.Domain.ProductCategoryAgg.ProductCategory category) => new() { Id = category.Id, Name = category.Name, Description = category.Description, PictureUrl = category.PictureUrl, PictureAlt = category.PictureAlt, PictureTitle = category.PictureTitle, MetaDescription = category.MetaDescription, Keywords = category.Keywords, Slug = category.Slug, Products = category.Products.Select(MapProduct).ToList() };
    private static ProductQueryModel MapProduct(Product product) => new() { Id = product.Id, Category = product.Category.Name, Name = product.Name, PictureAlt = product.PictureAlt, PictureTitle = product.PictureTitle, PictureUrl = product.PictureUrl, Slug = product.Slug };
    private static void ApplyPrices(IEnumerable<ProductQueryModel> products, IEnumerable<InventorySnapshot> inventories, IEnumerable<DiscountSnapshot> discounts)
    {
        foreach (var product in products)
        {
            var inventory = inventories.FirstOrDefault(x => x.ProductId == product.Id);
            if (inventory is null) continue;
            product.Price = inventory.UnitPrice.ToMoney(); product.InStock = inventory.InStock;
            var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
            if (discount is null) continue;
            product.DiscountRate = discount.DiscountRate; product.DiscountExpireDate = discount.EndDate.ToDiscountFormat(); product.HasDiscount = discount.DiscountRate > 0;
            product.PriceWithDiscount = (inventory.UnitPrice - Math.Round(inventory.UnitPrice * discount.DiscountRate / 100)).ToMoney();
        }
    }
}