using AwesomeAssertions;
using CommentManagement.Application.Contracts.Commands.Comments.AddComment;
using CommentManagement.Application.Contracts.Commands.Comments.ConfirmComment;
using CommentManagement.Contracts;
using CommentManagement.Infrastructure.EFCore;
using LampShade.BuildingBlocks.Application;
using LampShade.IntegrationTests.Fixtures;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.ReadModel.Queries.ProductCategories.GetProductCategoryWithProducts;
using ShopManagement.Application.ReadModel.Queries.Products.GetLatestArrivals;
using ShopManagement.Application.ReadModel.Queries.Products.GetProductBySlug;
using Xunit;

namespace LampShade.IntegrationTests.Shop;

/// <summary>Storefront pages compose Shop's catalog with Inventory prices, Discount rates and Comment reviews through module APIs.</summary>
[Collection(nameof(ModularMonolithCollection))]
public class ShopReadModelTests(ModularMonolithFixture app)
{
    [Fact]
    public async Task ProductDetails_ComposePriceDiscountAndConfirmedComments_FromOtherModules()
    {
        var category = await app.CategoryAsync();
        var product = await app.ProductAsync(category);
        await app.StockAsync(product.Id, 200_000, 5);
        await app.CustomerDiscountAsync(product.Id, 10);
        await app.SendAsync(new AddCommentCommand { Name = "Customer", Email = "customer@example.com", Description = "Great lamp", Rating = 5, OwnerRecordId = product.Id, Type = CommentOwnerType.Product });
        await app.SendAsync(new AddCommentCommand { Name = "Spammer", Email = "spam@example.com", Description = "Not confirmed", Rating = 1, OwnerRecordId = product.Id, Type = CommentOwnerType.Product });
        var confirmedId = await app.WithAsync<CommentContext, long>(c => c.Comments.Where(x => x.OwnerRecordId == product.Id && x.Name == "Customer").Select(x => x.Id).SingleAsync(TestContext.Current.CancellationToken));
        app.Auth.SignInAs(1, Roles.Administrator);
        await app.SendAsync(new ConfirmCommentCommand { Id = confirmedId });

        var details = await app.SendAsync(new GetProductBySlugQuery { Slug = product.Slug });
        var latest = await app.SendAsync(new GetLatestArrivalsQuery());
        var categoryPage = await app.SendAsync(new GetProductCategoryWithProductsQuery { Slug = category.Slug });

        details.Name.Should().Be(product.Name);
        details.DoublePrice.Should().Be(200_000);
        details.InStock.Should().BeTrue();
        details.HasDiscount.Should().BeTrue();
        details.DiscountRate.Should().Be(10);
        details.Comments.Should().ContainSingle().Which.Description.Should().Be("Great lamp");
        latest.Should().Contain(x => x.Id == product.Id);
        categoryPage.Products.Should().ContainSingle(x => x.Id == product.Id);
    }
}
