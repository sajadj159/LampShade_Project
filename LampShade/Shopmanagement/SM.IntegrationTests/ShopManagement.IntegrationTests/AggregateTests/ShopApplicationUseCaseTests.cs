using AccountManagement.Infrastructure.EFCore;
using AwesomeAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Order;
using ShopManagement.Application.Product;
using ShopManagement.Application.ProductCategory;
using ShopManagement.Application.ProductPicture;
using ShopManagement.Application.Slide;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Infrastructure.EFCore;
using ShopManagement.Infrastructure.EFCore.Repository;
using ShopManagement.IntegrationTests.Fixtures;
using Xunit;

namespace ShopManagement.IntegrationTests.AggregateTests;

[Collection(nameof(PostgreSqlCollection))]
public class ShopApplicationUseCaseTests(PostgreSqlFixture database)
{
    [Fact]
    public async Task ProductCategory_CreateEditAndDelete_ChangesPostgreSqlState()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await using var context = CreateShopContext();
        await context.Database.MigrateAsync(cancellationToken);
        var application = new ProductCategoryApplication(new ProductCategoryRepository(context), new TestFileUploader());
        var categorySuffix = Guid.NewGuid().ToString("N");

        var create = await application.CreateAsync(new ShopManagement.Application.Contract.ProductCategory.CreateProductCategory
        {
            Name = $"Lighting {categorySuffix}", Description = "Lighting products", PictureUrl = CreateImage("category.jpg"), PictureAlt = "Lighting", PictureTitle = "Lighting image", Keywords = "lighting", MetaDescription = "Lighting products", Slug = $"lighting-{categorySuffix}"
        }, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        var category = await context.ProductCategories.SingleAsync(item => item.Name == $"Lighting {categorySuffix}", cancellationToken);

        var edit = await application.EditAsync(new ShopManagement.Application.Contract.ProductCategory.EditProductCategory
        {
            Id = category.Id, Name = $"Indoor lighting {categorySuffix}", Description = "Indoor lighting products", PictureUrl = CreateImage("indoor.jpg"), PictureAlt = "Indoor", PictureTitle = "Indoor image", Keywords = "indoor", MetaDescription = "Indoor lighting products", Slug = $"indoor-lighting-{categorySuffix}"
        }, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        context.ChangeTracker.Clear();
        var edited = await context.ProductCategories.SingleAsync(item => item.Id == category.Id, cancellationToken);

        var delete = await application.DeleteAsync(edited.Id, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        create.IsSucceeded.Should().BeTrue();
        edit.IsSucceeded.Should().BeTrue();
        edited.Slug.Should().Be($"indoor-lighting-{categorySuffix}");
        delete.IsSucceeded.Should().BeTrue();
        (await context.ProductCategories.AnyAsync(item => item.Id == category.Id, cancellationToken)).Should().BeFalse();
    }

    [Fact]
    public async Task Product_CreateAndEdit_PersistsProductAndAdditionalPictures()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await using var context = CreateShopContext();
        await context.Database.MigrateAsync(cancellationToken);
        var category = await AddCategoryAsync(context, cancellationToken);
        var productCode = $"P{Guid.NewGuid():N}"[..15];
        var application = new ProductApplication(new ProductRepository(context), new TestFileUploader(), new ProductCategoryRepository(context), new ProductPictureRepository(context));

        var create = await application.CreateAsync(new ShopManagement.Application.Contract.A.Product.CreateProduct
        {
            Name = "Desk lamp", Code = productCode, ShortDescription = "Compact lamp", Description = "LED desk lamp", PictureUrl = CreateImage("lamp.jpg"), AdditionalPictures = [CreateImage("lamp-side.jpg")], PictureTitle = "Desk lamp", PictureAlt = "Desk lamp", Slug = "desk-lamp", Keywords = "lamp", MetaDescription = "Desk lamp", CategoryId = category.Id
        }, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        var product = await context.Products.Include(item => item.ProductPictures).SingleAsync(item => item.Code == productCode, cancellationToken);

        var edit = await application.EditAsync(new ShopManagement.Application.Contract.A.Product.EditProduct
        {
            Id = product.Id, Name = "Updated desk lamp", Code = $"E{Guid.NewGuid():N}"[..15], ShortDescription = "Updated compact lamp", Description = "Updated LED desk lamp", PictureUrl = CreateImage("updated-lamp.jpg"), PictureTitle = "Updated desk lamp", PictureAlt = "Updated desk lamp", Slug = "updated-desk-lamp", Keywords = "updated lamp", MetaDescription = "Updated desk lamp", CategoryId = category.Id
        }, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        context.ChangeTracker.Clear();
        var edited = await context.Products.SingleAsync(item => item.Id == product.Id, cancellationToken);

        create.IsSucceeded.Should().BeTrue();
        product.ProductPictures.Should().ContainSingle();
        edit.IsSucceeded.Should().BeTrue();
        edited.Name.Should().Be("Updated desk lamp");
        edited.Code.Should().HaveLength(15);
    }

    [Fact]
    public async Task ProductPicture_CreateEditRemoveAndRestore_ChangesPictureState()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await using var context = CreateShopContext();
        await context.Database.MigrateAsync(cancellationToken);
        var product = await AddProductAsync(context, cancellationToken);
        var application = new ProductPictureApplication(new ProductPictureRepository(context), new ProductRepository(context), new TestFileUploader());

        var create = await application.CreateAsync(new ShopManagement.Application.Contract.A.ProductPicture.CreateProductPicture { ProductId = product.Id, PictureUrl = CreateImage("detail.jpg"), PictureAlt = "Detail", PictureTitle = "Detail image" }, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        var picture = await context.ProductPictures.SingleAsync(item => item.PictureTitle == "Detail image", cancellationToken);
        var edit = await application.EditAsync(new ShopManagement.Application.Contract.A.ProductPicture.EditProductPicture { Id = picture.Id, ProductId = product.Id, PictureUrl = CreateImage("updated-detail.jpg"), PictureAlt = "Updated detail", PictureTitle = "Updated detail image" }, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        var remove = await application.RemoveAsync(picture.Id, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        var restore = await application.RestoreAsync(picture.Id, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        context.ChangeTracker.Clear();
        var restored = await context.ProductPictures.SingleAsync(item => item.Id == picture.Id, cancellationToken);

        create.IsSucceeded.Should().BeTrue(); edit.IsSucceeded.Should().BeTrue(); remove.IsSucceeded.Should().BeTrue(); restore.IsSucceeded.Should().BeTrue();
        restored.PictureTitle.Should().Be("Updated detail image"); restored.IsRemoved.Should().BeFalse();
    }

    [Fact]
    public async Task Slide_CreateEditRemoveAndRestore_ChangesSlideState()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await using var context = CreateShopContext();
        await context.Database.MigrateAsync(cancellationToken);
        var application = new SlideApplication(new SlideRepository(context), new TestFileUploader());

        var create = await application.CreateAsync(new ShopManagement.Application.Contract.A.Slide.CreateSlide { PictureUrl = CreateImage("slide.jpg"), PictureTitle = "Welcome", PictureAlt = "Welcome", Heading = "Welcome", Title = "Shop", Text = "Browse products", BtnText = "Browse", Link = "/products" }, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        var slide = await context.Slides.SingleAsync(item => item.PictureTitle == "Welcome", cancellationToken);
        var edit = await application.EditAsync(new ShopManagement.Application.Contract.A.Slide.EditSlide { Id = slide.Id, PictureUrl = CreateImage("updated-slide.jpg"), PictureTitle = "Updated welcome", PictureAlt = "Updated welcome", Heading = "Updated heading", Title = "Updated title", Text = "Updated text", BtnText = "Explore", Link = "/updated-products" }, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        var remove = await application.RemoveAsync(slide.Id, cancellationToken); await context.SaveChangesAsync(cancellationToken);
        var restore = await application.RestoreAsync(slide.Id, cancellationToken); await context.SaveChangesAsync(cancellationToken);
        context.ChangeTracker.Clear(); var restored = await context.Slides.SingleAsync(item => item.Id == slide.Id, cancellationToken);

        create.IsSucceeded.Should().BeTrue(); edit.IsSucceeded.Should().BeTrue(); remove.IsSucceeded.Should().BeTrue(); restore.IsSucceeded.Should().BeTrue();
        restored.Heading.Should().Be("Updated heading"); restored.IsRemoved.Should().BeFalse();
    }

    [Fact]
    public async Task Order_PlacePayApproveAndCancel_ChangesOrderState()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await using var shopContext = CreateShopContext();
        await using var accountContext = CreateAccountContext();
        await shopContext.Database.MigrateAsync(cancellationToken);
        var sms = new TestSmsService();
        var application = new OrderApplication(new OrderRepository(shopContext, accountContext), new TestAuthHelper(), new SuccessfulInventoryAcl(), sms, new TestAccountAcl());
        var order = new Order(1, 100_000, 1, 10_000, 90_000); order.AddItem(new OrderItem(1, 1, 100_000, 10)); shopContext.Orders.Add(order); await shopContext.SaveChangesAsync(cancellationToken);

        var upload = await application.UploadPaymentProofAsync(order.Id, "proof.jpg", cancellationToken); await shopContext.SaveChangesAsync(cancellationToken);
        var issueNumber = await application.ApprovePaymentProofAsync(order.Id, cancellationToken); await shopContext.SaveChangesAsync(cancellationToken);
        await application.CancelAsync(order.Id, cancellationToken); await shopContext.SaveChangesAsync(cancellationToken);
        shopContext.ChangeTracker.Clear();
        var persisted = await shopContext.Orders.SingleAsync(item => item.Id == order.Id, cancellationToken);

        upload.IsSucceeded.Should().BeTrue(); issueNumber.Should().NotBeNullOrWhiteSpace(); persisted.IsPaid.Should().BeTrue(); persisted.IsCanceled.Should().BeTrue(); sms.Messages.Should().ContainSingle();
    }

    [Fact]
    public async Task Order_PlacePaymentSucceededAndCashOnDelivery_ConfirmOrders()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await using var shopContext = CreateShopContext();
        await using var accountContext = CreateAccountContext();
        await shopContext.Database.MigrateAsync(cancellationToken);
        var sms = new TestSmsService();
        var application = new OrderApplication(new OrderRepository(shopContext, accountContext), new TestAuthHelper(), new SuccessfulInventoryAcl(), sms, new TestAccountAcl());
        var cart = new ShopManagement.Application.Contract.Order.Cart();
        cart.Add(new ShopManagement.Application.Contract.Order.CartItem { Id = 1, Count = 1, UnitPrice = 100_000, DiscountRate = 10 });
        cart.SetPaymentMethod(1);

        await application.PlaceOrderAsync(cart, cancellationToken);
        await shopContext.SaveChangesAsync(cancellationToken);
        var onlineOrder = await shopContext.Orders.OrderByDescending(item => item.Id).FirstAsync(item => item.AccountId == 1 && item.PaymentMethod == 1, cancellationToken);
        var onlineIssueNumber = await application.PaymentSucceededAsync(onlineOrder.Id, 12345, cancellationToken);
        await shopContext.SaveChangesAsync(cancellationToken);

        var cashOrder = new Order(1, 100_000, 2, 0, 100_000);
        cashOrder.AddItem(new OrderItem(1, 1, 100_000, 0));
        shopContext.Orders.Add(cashOrder);
        await shopContext.SaveChangesAsync(cancellationToken);
        var cashIssueNumber = await application.ApproveCashOnDeliveryAsync(cashOrder.Id, cancellationToken);
        await shopContext.SaveChangesAsync(cancellationToken);
        shopContext.ChangeTracker.Clear();

        var persistedOnlineOrder = await shopContext.Orders.SingleAsync(item => item.Id == onlineOrder.Id, cancellationToken);
        var persistedCashOrder = await shopContext.Orders.SingleAsync(item => item.Id == cashOrder.Id, cancellationToken);
        persistedOnlineOrder.IsPaid.Should().BeTrue();
        persistedOnlineOrder.RefId.Should().Be(12345);
        onlineIssueNumber.Should().NotBeNullOrWhiteSpace();
        persistedCashOrder.IsPaid.Should().BeTrue();
        cashIssueNumber.Should().NotBeNullOrWhiteSpace();
        sms.Messages.Should().HaveCount(2);
    }
    private ShopContext CreateShopContext() => new(new DbContextOptionsBuilder<ShopContext>().UseNpgsql(database.ConnectionString).Options);
    private AccountContext CreateAccountContext() => new(new DbContextOptionsBuilder<AccountContext>().UseNpgsql(database.ConnectionString).Options);
    private static IFormFile CreateImage(string name) => new FormFile(new MemoryStream([1]), 0, 1, "file", name) { Headers = new HeaderDictionary(), ContentType = "image/jpeg" };
    private static async Task<ProductCategory> AddCategoryAsync(ShopContext context, CancellationToken cancellationToken) { var category = new ProductCategory("Lighting", "Lighting", "lighting.jpg", "Lighting", "Lighting", "lighting", "Lighting", "lighting"); context.ProductCategories.Add(category); await context.SaveChangesAsync(cancellationToken); return category; }
    private static async Task<Product> AddProductAsync(ShopContext context, CancellationToken cancellationToken) { var category = await AddCategoryAsync(context, cancellationToken); var product = new Product("Desk lamp", "LMP-001", "Lamp", "Lamp", "lamp.jpg", "Lamp", "Lamp", "desk-lamp", "lamp", "Lamp", category.Id); context.Products.Add(product); await context.SaveChangesAsync(cancellationToken); return product; }
}