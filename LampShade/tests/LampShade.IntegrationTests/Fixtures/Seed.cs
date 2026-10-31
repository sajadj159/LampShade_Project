using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Infrastructure.EFCore;
using Xunit;

namespace LampShade.IntegrationTests.Fixtures;

/// <summary>Test data written straight into each module's own context, the way an admin would have set it up.</summary>
public static class Seed
{
    public static Task<ProductCategory> CategoryAsync(this ModularMonolithFixture fixture) =>
        fixture.WithAsync<ShopContext, ProductCategory>(async context =>
        {
            var suffix = Guid.NewGuid().ToString("N");
            var category = new ProductCategory($"Category {suffix}", "Description", "category.jpg", "Category", "Category", "category", "Description", $"category-{suffix}");
            context.ProductCategories.Add(category);
            await context.SaveChangesAsync(TestContext.Current.CancellationToken);
            return category;
        });

    public static async Task<Product> ProductAsync(this ModularMonolithFixture fixture, ProductCategory? category = null)
    {
        category ??= await fixture.CategoryAsync();
        return await fixture.WithAsync<ShopContext, Product>(async context =>
        {
            var suffix = Guid.NewGuid().ToString("N");
            var product = new Product($"Product {suffix}", $"P{suffix}"[..15], "Short", "Description", "product.jpg", "Product", "Product", $"product-{suffix}", "product", "Description", category.Id);
            context.Products.Add(product);
            await context.SaveChangesAsync(TestContext.Current.CancellationToken);
            return product;
        });
    }

    public static Task<InventoryManagement.Domain.InventoryAgg.Inventory> StockAsync(this ModularMonolithFixture fixture, long productId, double unitPrice, int count) =>
        fixture.WithAsync<InventoryContext, InventoryManagement.Domain.InventoryAgg.Inventory>(async context =>
        {
            var inventory = new InventoryManagement.Domain.InventoryAgg.Inventory(productId, unitPrice);
            if (count > 0) inventory.Increase(count, 1, "Seed stock");
            context.Inventory.Add(inventory);
            await context.SaveChangesAsync(TestContext.Current.CancellationToken);
            return inventory;
        });

    public static Task CustomerDiscountAsync(this ModularMonolithFixture fixture, long productId, int rate) =>
        fixture.WithAsync<DiscountContext>(async context =>
        {
            context.CustomerDiscounts.Add(new CustomerDiscount(productId, rate, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(1), "Campaign"));
            await context.SaveChangesAsync(TestContext.Current.CancellationToken);
        });
}
