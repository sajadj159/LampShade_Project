using AwesomeAssertions;
using LampShade.BuildingBlocks.Application;
using LampShade.IntegrationTests.Fixtures;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Commands.ProductCategories.CreateProductCategory;
using ShopManagement.Application.Contracts.Commands.ProductCategories.DeleteProductCategory;
using ShopManagement.Application.Contracts.Commands.ProductCategories.EditProductCategory;
using ShopManagement.Application.Contracts.Commands.ProductPictures.CreateProductPicture;
using ShopManagement.Application.Contracts.Commands.ProductPictures.EditProductPicture;
using ShopManagement.Application.Contracts.Commands.ProductPictures.RemoveProductPicture;
using ShopManagement.Application.Contracts.Commands.ProductPictures.RestoreProductPicture;
using ShopManagement.Application.Contracts.Commands.Products.CreateProduct;
using ShopManagement.Application.Contracts.Commands.Products.EditProduct;
using ShopManagement.Application.Contracts.Commands.Slides.CreateSlide;
using ShopManagement.Application.Contracts.Commands.Slides.EditSlide;
using ShopManagement.Application.Contracts.Commands.Slides.RemoveSlide;
using ShopManagement.Application.Contracts.Commands.Slides.RestoreSlide;
using ShopManagement.Infrastructure.EFCore;
using Xunit;
using static LampShade.IntegrationTests.Fixtures.ModularMonolithFixture;

namespace LampShade.IntegrationTests.Shop;

[Collection(nameof(ModularMonolithCollection))]
public class CatalogUseCaseTests(ModularMonolithFixture app)
{
    private CancellationToken CancellationToken => TestContext.Current.CancellationToken;

    [Fact]
    public async Task ProductCategory_CreateEditAndDelete_ChangesPostgreSqlState()
    {
        var suffix = Guid.NewGuid().ToString("N");

        var create = await app.SendAsync(new CreateProductCategoryCommand { Name = $"Lighting {suffix}", Description = "Lighting products", PictureUrl = Image("category.jpg"), PictureAlt = "Lighting", PictureTitle = "Lighting image", Keywords = "lighting", MetaDescription = "Lighting products", Slug = $"lighting-{suffix}" });
        var category = await app.WithAsync<ShopContext, ShopManagement.Domain.ProductCategoryAgg.ProductCategory>(c => c.ProductCategories.SingleAsync(x => x.Name == $"Lighting {suffix}", CancellationToken));
        var edit = await app.SendAsync(new EditProductCategoryCommand { Id = category.Id, Name = $"Indoor lighting {suffix}", Description = "Indoor lighting products", PictureUrl = Image("indoor.jpg"), PictureAlt = "Indoor", PictureTitle = "Indoor image", Keywords = "indoor", MetaDescription = "Indoor lighting products", Slug = $"indoor-lighting-{suffix}" });
        var edited = await app.WithAsync<ShopContext, ShopManagement.Domain.ProductCategoryAgg.ProductCategory>(c => c.ProductCategories.SingleAsync(x => x.Id == category.Id, CancellationToken));
        var delete = await app.SendAsync(new DeleteProductCategoryCommand { Id = category.Id });

        create.IsSucceeded.Should().BeTrue();
        edit.IsSucceeded.Should().BeTrue();
        edited.Slug.Should().Be($"indoor-lighting-{suffix}");
        delete.IsSucceeded.Should().BeTrue();
        (await app.WithAsync<ShopContext, bool>(c => c.ProductCategories.AnyAsync(x => x.Id == category.Id, CancellationToken))).Should().BeFalse();
    }

    [Fact]
    public async Task Product_CreateAndEdit_PersistsProductAndAdditionalPictures_AndSanitizesDescription()
    {
        var category = await app.CategoryAsync();
        var code = $"P{Guid.NewGuid():N}"[..15];

        var create = await app.SendAsync(new CreateProductCommand { Name = Unique("Desk lamp"), Code = code, ShortDescription = "Compact lamp", Description = "<p>LED desk lamp</p><script>alert('x')</script>", PictureUrl = Image("lamp.jpg"), AdditionalPictures = [Image("lamp-side.jpg")], PictureTitle = "Desk lamp", PictureAlt = "Desk lamp", Slug = Unique("desk-lamp"), Keywords = "lamp", MetaDescription = "Desk lamp", CategoryId = category.Id });
        var product = await app.WithAsync<ShopContext, ShopManagement.Domain.ProductAgg.Product>(c => c.Products.Include(x => x.ProductPictures).SingleAsync(x => x.Code == code, CancellationToken));
        var edit = await app.SendAsync(new EditProductCommand { Id = product.Id, Name = Unique("Updated desk lamp"), Code = $"E{Guid.NewGuid():N}"[..15], ShortDescription = "Updated", Description = "<img src=x onerror=alert(1)>Updated", PictureUrl = Image("updated-lamp.jpg"), PictureTitle = "Updated desk lamp", PictureAlt = "Updated desk lamp", Slug = Unique("updated-desk-lamp"), Keywords = "updated lamp", MetaDescription = "Updated desk lamp", CategoryId = category.Id });
        var edited = await app.WithAsync<ShopContext, ShopManagement.Domain.ProductAgg.Product>(c => c.Products.SingleAsync(x => x.Id == product.Id, CancellationToken));

        create.IsSucceeded.Should().BeTrue();
        product.ProductPictures.Should().ContainSingle();
        product.Description.Should().Contain("LED desk lamp").And.NotContain("<script");
        edit.IsSucceeded.Should().BeTrue();
        edited.Name.Should().StartWith("Updated desk lamp");
        edited.Description.Should().NotContain("onerror");
    }

    [Fact]
    public async Task ProductPicture_CreateEditRemoveAndRestore_ChangesPictureState()
    {
        var product = await app.ProductAsync();
        var title = Unique("Detail");

        var create = await app.SendAsync(new CreateProductPictureCommand { ProductId = product.Id, PictureUrl = Image("detail.jpg"), PictureAlt = "Detail", PictureTitle = title });
        var picture = await app.WithAsync<ShopContext, ShopManagement.Domain.ProductPictureAgg.ProductPicture>(c => c.ProductPictures.SingleAsync(x => x.PictureTitle == title, CancellationToken));
        var edit = await app.SendAsync(new EditProductPictureCommand { Id = picture.Id, ProductId = product.Id, PictureUrl = Image("updated-detail.jpg"), PictureAlt = "Updated detail", PictureTitle = $"{title} updated" });
        var remove = await app.SendAsync(new RemoveProductPictureCommand { Id = picture.Id });
        var restore = await app.SendAsync(new RestoreProductPictureCommand { Id = picture.Id });
        var restored = await app.WithAsync<ShopContext, ShopManagement.Domain.ProductPictureAgg.ProductPicture>(c => c.ProductPictures.SingleAsync(x => x.Id == picture.Id, CancellationToken));

        new[] { create, edit, remove, restore }.Should().AllSatisfy(result => result.IsSucceeded.Should().BeTrue());
        restored.PictureTitle.Should().Be($"{title} updated");
        restored.IsRemoved.Should().BeFalse();
    }

    [Fact]
    public async Task Slide_CreateEditRemoveAndRestore_ChangesSlideState()
    {
        var title = Unique("Welcome");

        var create = await app.SendAsync(new CreateSlideCommand { PictureUrl = Image("slide.jpg"), PictureTitle = title, PictureAlt = "Welcome", Heading = "Welcome", Title = "Shop", Text = "Browse products", BtnText = "Browse", Link = "/products" });
        var slide = await app.WithAsync<ShopContext, ShopManagement.Domain.SlideAgg.Slide>(c => c.Slides.SingleAsync(x => x.PictureTitle == title, CancellationToken));
        var edit = await app.SendAsync(new EditSlideCommand { Id = slide.Id, PictureUrl = Image("updated-slide.jpg"), PictureTitle = $"{title} updated", PictureAlt = "Updated welcome", Heading = "Updated heading", Title = "Updated title", Text = "Updated text", BtnText = "Explore", Link = "/updated-products" });
        var remove = await app.SendAsync(new RemoveSlideCommand { Id = slide.Id });
        var restore = await app.SendAsync(new RestoreSlideCommand { Id = slide.Id });
        var restored = await app.WithAsync<ShopContext, ShopManagement.Domain.SlideAgg.Slide>(c => c.Slides.SingleAsync(x => x.Id == slide.Id, CancellationToken));

        new[] { create, edit, remove, restore }.Should().AllSatisfy(result => result.IsSucceeded.Should().BeTrue());
        restored.Heading.Should().Be("Updated heading");
        restored.IsRemoved.Should().BeFalse();
    }
}
