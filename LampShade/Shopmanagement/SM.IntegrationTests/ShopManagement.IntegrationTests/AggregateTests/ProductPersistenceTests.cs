using AwesomeAssertions;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Infrastructure.EFCore;
using ShopManagement.IntegrationTests.Fixtures;
using Xunit;

namespace ShopManagement.IntegrationTests.AggregateTests;

[Collection(nameof(PostgreSqlCollection))]
public class ProductPersistenceTests(PostgreSqlFixture database)
{
    [Fact]
    public async Task SaveChanges_PersistsProductWithItsCategoryInPostgreSql()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await using var context = CreateContext();
        await context.Database.MigrateAsync(cancellationToken);

        var category = new ProductCategory(
            "Lighting", "Lighting products", "lighting.jpg", "Lighting alt", "Lighting title",
            "lighting, lamp", "Lighting meta description", "lighting");
        context.ProductCategories.Add(category);
        await context.SaveChangesAsync(cancellationToken);

        var product = new Product(
            "Desk lamp", "LMP-001", "A compact desk lamp", "A compact LED desk lamp", "desk-lamp.jpg",
            "Desk lamp", "Desk lamp image", "desk-lamp", "lamp, desk", "Desk lamp meta description", category.Id);
        context.Products.Add(product);
        await context.SaveChangesAsync(cancellationToken);
        context.ChangeTracker.Clear();

        var persistedProduct = await context.Products
            .Include(item => item.Category)
            .SingleAsync(item => item.Code == "LMP-001", cancellationToken);

        persistedProduct.Name.Should().Be("Desk lamp");
        persistedProduct.CategoryId.Should().Be(category.Id);
        persistedProduct.Category.Name.Should().Be("Lighting");
    }

    private ShopContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<ShopContext>()
            .UseNpgsql(database.ConnectionString)
            .Options;

        return new ShopContext(options);
    }
}