using AwesomeAssertions;
using LampShade.ReadModel.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.EFCore;
using InventoryManagement.IntegrationTests.Fixtures;
using LampShade.ReadModel.Application.Query;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Infrastructure.EFCore;
using ShopManagement.Infrastructure.EFCore.ReadAcl;
using Xunit;

namespace InventoryManagement.IntegrationTests.AggregateTests;

[Collection(nameof(PostgreSqlCollection))]
public class InventoryReadModelTests(PostgreSqlFixture database)
{
    [Fact]
    public async Task CheckStock_ReturnsAvailabilityAndProductNameFromPostgreSql()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await using var inventoryContext = CreateInventoryContext();
        await using var shopContext = CreateShopContext();
        await inventoryContext.Database.MigrateAsync(cancellationToken);
        await shopContext.Database.MigrateAsync(cancellationToken);
        var suffix = Guid.NewGuid().ToString("N");
        var category = new ProductCategory($"Inventory {suffix}", "Products", "category.jpg", "Category", "Category", "inventory", "Products", $"inventory-{suffix}");
        shopContext.ProductCategories.Add(category);
        await shopContext.SaveChangesAsync(cancellationToken);
        var product = new Product($"Stock product {suffix}", $"P{suffix}"[..15], "Stock", "Stock product", "product.jpg", "Product", "Product", $"stock-{suffix}", "stock", "Stock", category.Id);
        shopContext.Products.Add(product);
        await shopContext.SaveChangesAsync(cancellationToken);
        var inventory = new Inventory(product.Id, 200_000);
        inventory.Increase(3, 1, "Seed stock");
        inventoryContext.Inventory.Add(inventory);
        await inventoryContext.SaveChangesAsync(cancellationToken);
        var query = new InventoryQuery(inventoryContext, new InventoryProductReadAcl(shopContext));

        var available = await query.CheckStockAsync(new IsInStock { ProductId = product.Id, Count = 2 }, cancellationToken);
        var unavailable = await query.CheckStockAsync(new IsInStock { ProductId = product.Id, Count = 4 }, cancellationToken);

        available.IsStock.Should().BeTrue();
        unavailable.IsStock.Should().BeFalse();
        unavailable.ProductName.Should().Be(product.Name);
    }

    private InventoryContext CreateInventoryContext() => new(new DbContextOptionsBuilder<InventoryContext>().UseNpgsql(database.ConnectionString).Options);
    private ShopContext CreateShopContext() => new(new DbContextOptionsBuilder<ShopContext>().UseNpgsql(database.ConnectionString).Options);
}