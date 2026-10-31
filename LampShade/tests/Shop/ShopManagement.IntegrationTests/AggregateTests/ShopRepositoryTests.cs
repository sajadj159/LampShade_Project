using AccountManagement.Infrastructure.EFCore;
using AwesomeAssertions;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contract.A.Product;
using ShopManagement.Application.Contract.A.ProductPicture;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.SlideAgg;
using ShopManagement.Infrastructure.EFCore;
using ShopManagement.Infrastructure.EFCore.Repository;
using ShopManagement.IntegrationTests.Fixtures;
using Xunit;

namespace ShopManagement.IntegrationTests.AggregateTests;

[Collection(nameof(PostgreSqlCollection))]
public class ShopRepositoryTests(PostgreSqlFixture database)
{
    [Fact]
    public async Task ProductCategoryRepository_ReturnsDetailsSlugsSearchAndUsage()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await using var context = CreateShopContext();
        await context.Database.MigrateAsync(cancellationToken);
        var category = await AddCategoryAsync(context, cancellationToken);
        var product = await AddProductAsync(context, category, cancellationToken);
        var repository = new ProductCategoryRepository(context);

        repository.GetProductCategories().Should().Contain(item => item.Id == category.Id);
        repository.GetDetails(category.Id).Slug.Should().Be(category.Slug);
        repository.GetSlugBy(category.Id).Should().Be(category.Slug);
        (await repository.GetSlugByAsync(category.Id, cancellationToken)).Should().Be(category.Slug);
        repository.HasProducts(category.Id).Should().BeTrue();
        (await repository.HasProductsAsync(category.Id, cancellationToken)).Should().BeTrue();
        repository.Search(new ProductCategorySearchModel { Name = category.Name }).Should().ContainSingle(item => item.Id == category.Id);
        product.CategoryId.Should().Be(category.Id);
    }

    [Fact]
    public async Task ProductAndPictureRepositories_ReturnDetailsSearchAndRelatedAggregates()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await using var context = CreateShopContext();
        await context.Database.MigrateAsync(cancellationToken);
        var category = await AddCategoryAsync(context, cancellationToken);
        var product = await AddProductAsync(context, category, cancellationToken);
        var picture = new ProductPicture(product.Id, "detail.jpg", "Detail", "Detail");
        context.ProductPictures.Add(picture);
        await context.SaveChangesAsync(cancellationToken);
        context.ChangeTracker.Clear();
        var products = new ProductRepository(context);
        var pictures = new ProductPictureRepository(context);

        products.GetDetails(product.Id).Code.Should().Be(product.Code);
        products.GetProducts().Should().Contain(item => item.Id == product.Id && item.CategoryId == category.Id);
        products.Search(new ProductSearchModel { Code = product.Code }).Should().ContainSingle(item => item.Id == product.Id);
        (await products.GetProductWithCategoriesAsync(product.Id, cancellationToken)).Category.Slug.Should().Be(category.Slug);
        pictures.GetDetails(picture.Id).ProductId.Should().Be(product.Id);
        pictures.Search(new ProductPictureSearchModel { ProductId = product.Id }).Should().ContainSingle(item => item.Id == picture.Id);
        (await pictures.GetWithProductsAndCategoriesAsync(picture.Id, cancellationToken)).Product.Category.Id.Should().Be(category.Id);
    }

    [Fact]
    public async Task SlideRepository_ReturnsDetailsAndList()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await using var context = CreateShopContext();
        await context.Database.MigrateAsync(cancellationToken);
        var slide = new Slide("slide.jpg", "Slide", "Slide", "Heading", "Title", "Text", "Browse", "/products");
        context.Slides.Add(slide);
        await context.SaveChangesAsync(cancellationToken);
        var repository = new SlideRepository(context);

        repository.GetDetails(slide.Id).Heading.Should().Be("Heading");
        repository.GetList().Should().ContainSingle(item => item.Id == slide.Id && item.Title == "Title");
    }

    [Fact]
    public async Task OrderRepository_ReturnsAmountSearchAndItems()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await using var shopContext = CreateShopContext();
        await using var accountContext = CreateAccountContext();
        await shopContext.Database.MigrateAsync(cancellationToken);
        await accountContext.Database.MigrateAsync(cancellationToken);
        var category = await AddCategoryAsync(shopContext, cancellationToken);
        var product = await AddProductAsync(shopContext, category, cancellationToken);
        var order = new Order(42, 100_000, 1, 10_000, 90_000);
        order.AddItem(new OrderItem(product.Id, 1, 100_000, 10));
        shopContext.Orders.Add(order);
        await shopContext.SaveChangesAsync(cancellationToken);
        shopContext.ChangeTracker.Clear();
        var repository = new OrderRepository(shopContext, accountContext);

        repository.GetAmountBy(order.Id).Should().Be(90_000);
        repository.Search(new ShopManagement.Application.Contract.Order.OrderSearchModel { AccountId = 42, IsCanceled = false }).Should().ContainSingle(item => item.Id == order.Id);
        repository.GetItemsBy(order.Id).Should().ContainSingle(item => item.ProductId == product.Id && item.ProductName == product.Name);
    }

    private ShopContext CreateShopContext() => new(new DbContextOptionsBuilder<ShopContext>().UseNpgsql(database.ConnectionString).Options);
    private AccountContext CreateAccountContext() => new(new DbContextOptionsBuilder<AccountContext>().UseNpgsql(database.ConnectionString).Options);
    private static async Task<ProductCategory> AddCategoryAsync(ShopContext context, CancellationToken cancellationToken)
    {
        var suffix = Guid.NewGuid().ToString("N");
        var category = new ProductCategory($"Category {suffix}", "Description", "category.jpg", "Category", "Category", "category", "Description", $"category-{suffix}");
        context.ProductCategories.Add(category);
        await context.SaveChangesAsync(cancellationToken);
        return category;
    }

    private static async Task<Product> AddProductAsync(ShopContext context, ProductCategory category, CancellationToken cancellationToken)
    {
        var suffix = Guid.NewGuid().ToString("N");
        var product = new Product($"Product {suffix}", $"P{suffix}"[..15], "Short", "Description", "product.jpg", "Product", "Product", $"product-{suffix}", "product", "Description", category.Id);
        context.Products.Add(product);
        await context.SaveChangesAsync(cancellationToken);
        return product;
    }
}