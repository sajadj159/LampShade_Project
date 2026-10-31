using AccountManagement.Infrastructure.EFCore;
using AwesomeAssertions;
using LampShade.IntegrationTests.Fixtures;
using ShopManagement.Application.Contract.A.Product;
using ShopManagement.Application.Contract.A.ProductPicture;
using ShopManagement.Application.Contract.Order;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Application.ReadModel.Admin;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.SlideAgg;
using ShopManagement.Infrastructure.EFCore;
using Xunit;

namespace LampShade.IntegrationTests.Shop;

/// <summary>The admin-panel projections behind Shop's read repositories.</summary>
[Collection(nameof(ModularMonolithCollection))]
public class ShopReadRepositoryTests(ModularMonolithFixture app)
{
    private CancellationToken CancellationToken => TestContext.Current.CancellationToken;

    [Fact]
    public async Task ProductCategories_ReturnDetailsSlugsSearchAndUsage()
    {
        var category = await app.CategoryAsync();
        var product = await app.ProductAsync(category);

        await app.WithAsync<IProductCategoryReadRepository>(repository =>
        {
            repository.GetProductCategories().Should().Contain(x => x.Id == category.Id);
            repository.GetDetails(category.Id).Slug.Should().Be(category.Slug);
            repository.Search(new ProductCategorySearchModel { Name = category.Name }).Should().ContainSingle(x => x.Id == category.Id);
            return Task.CompletedTask;
        });
        await app.WithAsync<IProductCategoryRepository>(async repository =>
        {
            (await repository.GetSlugByAsync(category.Id, CancellationToken)).Should().Be(category.Slug);
            (await repository.HasProductsAsync(category.Id, CancellationToken)).Should().BeTrue();
        });
        product.CategoryId.Should().Be(category.Id);
    }

    [Fact]
    public async Task ProductsAndPictures_ReturnDetailsSearchAndRelatedAggregates()
    {
        var category = await app.CategoryAsync();
        var product = await app.ProductAsync(category);
        var picture = await app.WithAsync<ShopContext, ProductPicture>(async c =>
        {
            var created = new ProductPicture(product.Id, "detail.jpg", "Detail", "Detail");
            c.ProductPictures.Add(created);
            await c.SaveChangesAsync(CancellationToken);
            return created;
        });

        await app.WithAsync<IProductReadRepository>(products =>
        {
            products.GetDetails(product.Id).Code.Should().Be(product.Code);
            products.GetProducts().Should().Contain(x => x.Id == product.Id && x.CategoryId == category.Id);
            products.Search(new ProductSearchModel { Code = product.Code }).Should().ContainSingle(x => x.Id == product.Id);
            return Task.CompletedTask;
        });
        await app.WithAsync<IProductPictureReadRepository>(pictures =>
        {
            pictures.GetDetails(picture.Id).ProductId.Should().Be(product.Id);
            pictures.Search(new ProductPictureSearchModel { ProductId = product.Id }).Should().ContainSingle(x => x.Id == picture.Id);
            return Task.CompletedTask;
        });
        await app.WithAsync<IProductRepository>(async products =>
            (await products.GetProductWithCategoriesAsync(product.Id, CancellationToken)).Category.Slug.Should().Be(category.Slug));
        await app.WithAsync<IProductPictureRepository>(async pictures =>
            (await pictures.GetWithProductsAndCategoriesAsync(picture.Id, CancellationToken)).Product.Category.Id.Should().Be(category.Id));
    }

    [Fact]
    public async Task Slides_ReturnDetailsAndList()
    {
        var title = ModularMonolithFixture.Unique("Title");
        var slide = await app.WithAsync<ShopContext, Slide>(async c =>
        {
            var created = new Slide("slide.jpg", "Slide", "Slide", "Heading", title, "Text", "Browse", "/products");
            c.Slides.Add(created);
            await c.SaveChangesAsync(CancellationToken);
            return created;
        });

        await app.WithAsync<ISlideReadRepository>(repository =>
        {
            repository.GetDetails(slide.Id).Heading.Should().Be("Heading");
            repository.GetList().Should().ContainSingle(x => x.Id == slide.Id && x.Title == title);
            return Task.CompletedTask;
        });
    }

    [Fact]
    public async Task Orders_ReturnAmountSearchWithBuyerNameAndItems()
    {
        var product = await app.ProductAsync();
        var buyerId = await app.WithAsync<AccountContext, long>(async c =>
        {
            var account = new AccountManagement.Domain.AccountAgg.Account(ModularMonolithFixture.Unique("buyer"), "Order buyer", "hash", "09120000000", 2, string.Empty, string.Empty, string.Empty);
            c.Accounts.Add(account);
            await c.SaveChangesAsync(CancellationToken);
            return account.Id;
        });
        var order = await app.WithAsync<ShopContext, Order>(async c =>
        {
            var created = new Order(buyerId, 100_000, 1, 10_000, 90_000);
            created.AddItem(new OrderItem(product.Id, 1, 100_000, 10));
            c.Orders.Add(created);
            await c.SaveChangesAsync(CancellationToken);
            return created;
        });

        await app.WithAsync<IOrderReadRepository>(async repository =>
        {
            repository.GetAmountBy(order.Id).Should().Be(90_000);
            (await repository.SearchAsync(new OrderSearchModel { AccountId = buyerId, IsCanceled = false }, CancellationToken))
                .Should().ContainSingle(x => x.Id == order.Id && x.AccountFullName == "Order buyer");
            repository.GetItemsBy(order.Id).Should().ContainSingle(x => x.ProductId == product.Id && x.ProductName == product.Name);
        });
    }
}
