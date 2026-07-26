using AwesomeAssertions;
using _0_Framework.Domain;
using ShopManagement.Domain.ProductPictureAgg;
using Xunit;

namespace ShopManagement.Tests.AggregateTests;

public class ProductPictureTests
{
    [Fact] public void DefaultConstructor_InheritsFromEntityBase() => new ProductPicture().Should().BeAssignableTo<EntityBase>();
    [Fact] public void DefaultConstructor_SetsCreationDate() { var beforeCreation = DateTime.UtcNow; var picture = new ProductPicture(); picture.CreationDate.Should().BeOnOrAfter(beforeCreation); picture.CreationDate.Should().BeOnOrBefore(DateTime.UtcNow); }
    [Fact] public void DefaultConstructor_SetsDefaultId() => new ProductPicture().Id.Should().Be(0);
    [Fact] public void DefaultConstructor_LeavesProductUninitialized() => new ProductPicture().Product.Should().BeNull();
    [Fact] public void ParameterizedConstructor_SetsProductId() => CreatePicture().ProductId.Should().Be(1);
    [Fact] public void ParameterizedConstructor_SetsPictureUrl() => CreatePicture().PictureUrl.Should().Be("product.jpg");
    [Fact] public void ParameterizedConstructor_SetsPictureTitle() => CreatePicture().PictureTitle.Should().Be("Product title");
    [Fact] public void ParameterizedConstructor_SetsPictureAlt() => CreatePicture().PictureAlt.Should().Be("Product alt");
    [Fact] public void ParameterizedConstructor_SetsIsRemovedToFalse() => CreatePicture().IsRemoved.Should().BeFalse();
    [Fact]
    public void Edit_WithPictureUrl_UpdatesAllEditableProperties()
    {
        var picture = CreatePicture(); picture.Edit(2, "updated.jpg", "Updated title", "Updated alt");
        picture.ProductId.Should().Be(2); picture.PictureUrl.Should().Be("updated.jpg"); picture.PictureTitle.Should().Be("Updated title"); picture.PictureAlt.Should().Be("Updated alt");
    }
    [Fact] public void Edit_WithWhitespacePictureUrl_PreservesCurrentPicture() { var picture = CreatePicture(); picture.Edit(2, " ", "Updated title", "Updated alt"); picture.PictureUrl.Should().Be("product.jpg"); }
    [Fact] public void Remove_MarksPictureAsRemoved() { var picture = CreatePicture(); picture.Remove(); picture.IsRemoved.Should().BeTrue(); }
    [Fact] public void Restore_AfterRemove_MarksPictureAsActive() { var picture = CreatePicture(); picture.Remove(); picture.Restore(); picture.IsRemoved.Should().BeFalse(); }
    private static ProductPicture CreatePicture() => new(1, "product.jpg", "Product title", "Product alt");
}