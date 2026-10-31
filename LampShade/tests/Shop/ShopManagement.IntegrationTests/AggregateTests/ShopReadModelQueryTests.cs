using AwesomeAssertions;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore.ReadAcl;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Infrastructure.EFCore;
using DiscountManagement.Infrastructure.EFCore.ReadAcl;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore.ReadAcl;
using LampShade.ReadModel.Application.Query;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contract.Order;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.SlideAgg;
using ShopManagement.Infrastructure.EFCore;
using ShopManagement.IntegrationTests.Fixtures;
using Xunit;

namespace ShopManagement.IntegrationTests.AggregateTests;

[Collection(nameof(PostgreSqlCollection))]
public class ShopReadModelQueryTests(PostgreSqlFixture database)
{
    [Fact]
    public async Task ShopReadModelQueries_ReturnPersistedAndEnrichedData()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await using var shopContext = CreateShopContext();
        await using var inventoryContext = CreateInventoryContext();
        await using var discountContext = CreateDiscountContext();
        await using var commentContext = CreateCommentContext();
        await shopContext.Database.MigrateAsync(cancellationToken);
        await inventoryContext.Database.MigrateAsync(cancellationToken);
        await discountContext.Database.MigrateAsync(cancellationToken);
        await commentContext.Database.MigrateAsync(cancellationToken);

        var category = new ProductCategory("Lighting", "Lighting products", "category.jpg", "Lighting", "Lighting", "lighting", "Lighting products", "lighting");
        shopContext.ProductCategories.Add(category);
        await shopContext.SaveChangesAsync(cancellationToken);
        var product = new Product("Desk lamp", "LMP-QUERY", "Compact lamp", "LED desk lamp", "lamp.jpg", "Desk lamp", "Desk lamp", "desk-lamp-query", "lamp", "Desk lamp", category.Id);
        var slide = new Slide("slide.jpg", "Slide", "Slide", "Heading", "Title", "Text", "Browse", "/products");
        var paidOrder = new Order(7, 200_000, 1, 20_000, 180_000);
        paidOrder.AddItem(new OrderItem(product.Id, 1, 200_000, 10));
        paidOrder.PaymentSucceeded(12345);
        shopContext.AddRange(product, slide, paidOrder);
        await shopContext.SaveChangesAsync(cancellationToken);

        var inventory = new Inventory(product.Id, 200_000);
        inventory.Increase(5, 1, "Seed stock");
        inventoryContext.Inventory.Add(inventory);
        discountContext.CustomerDiscounts.Add(new CustomerDiscount(product.Id, 10, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(1), "Campaign"));
        var comment = new Comment("Customer", "customer@example.com", string.Empty, "Great lamp", 5, product.Id, CommentType.Product, null);
        comment.Confirm();
        commentContext.Comments.Add(comment);
        await inventoryContext.SaveChangesAsync(cancellationToken);
        await discountContext.SaveChangesAsync(cancellationToken);
        await commentContext.SaveChangesAsync(cancellationToken);

        var inventoryAcl = new ShopInventoryReadAcl(inventoryContext);
        var discountAcl = new ShopDiscountReadAcl(discountContext);
        var commentAcl = new ShopCommentReadAcl(commentContext);
        var productQuery = new ProductQuery(shopContext, inventoryAcl, discountAcl, commentAcl);
        var productDetails = await productQuery.GetProductDetailsAsync(product.Slug, cancellationToken);
        var latest = await productQuery.GetLatestArrivalsAsync(cancellationToken);
        var search = await productQuery.SearchAsync("Desk", cancellationToken);
        var stock = await productQuery.CheckInventoryStatusAsync([new CartItem { Id = product.Id, Count = 2 }], cancellationToken);
        var categoryQuery = new ProductCategoryQuery(shopContext, inventoryAcl, discountAcl);
        var categoryDetails = await categoryQuery.GetProductCategoryWithProductsAsync(category.Slug, cancellationToken);
        var categories = await categoryQuery.GetProductCategoryQueriesAsync(cancellationToken);
        var categoriesWithProducts = await categoryQuery.GetProductCategoriesWithProductsAsync(cancellationToken);
        var slides = await new SlideQuery(shopContext).GetSlidesAsync(cancellationToken);
        var orderQuery = new OrderQuery(shopContext);
        var paidOrders = await orderQuery.GetPayedOrdersAsync(cancellationToken);
        var accountOrders = await orderQuery.GetOrdersAsync(7, cancellationToken);
        var cart = new CartCalculatorService(discountAcl, new TestAuthHelper()).ComputeCart([new CartItem { Id = product.Id, Count = 2, UnitPrice = 200_000 }]);

        productDetails.Name.Should().Be("Desk lamp");
        productDetails.HasDiscount.Should().BeTrue();
        productDetails.Comments.Should().ContainSingle();
        latest.Should().ContainSingle(item => item.Id == product.Id);
        search.Should().ContainSingle(item => item.Id == product.Id);
        stock.Should().ContainSingle().Which.IsInStock.Should().BeTrue();
        categoryDetails.Products.Should().ContainSingle(item => item.Id == product.Id);
        categories.Should().ContainSingle(item => item.Id == category.Id);
        categoriesWithProducts.Should().ContainSingle(item => item.Products.Any(productItem => productItem.Id == product.Id));
        slides.Should().ContainSingle(item => item.Id == slide.Id);
        paidOrders.Should().ContainSingle(item => item.Id == paidOrder.Id);
        accountOrders.Should().ContainSingle(item => item.Id == paidOrder.Id);
        cart.PayAmount.Should().Be(360_000);
    }

    private ShopContext CreateShopContext() => new(new DbContextOptionsBuilder<ShopContext>().UseNpgsql(database.ConnectionString).Options);
    private InventoryContext CreateInventoryContext() => new(new DbContextOptionsBuilder<InventoryContext>().UseNpgsql(database.ConnectionString).Options);
    private DiscountContext CreateDiscountContext() => new(new DbContextOptionsBuilder<DiscountContext>().UseNpgsql(database.ConnectionString).Options);
    private CommentContext CreateCommentContext() => new(new DbContextOptionsBuilder<CommentContext>().UseNpgsql(database.ConnectionString).Options);
}