using AwesomeAssertions;
using _0_Framework.Domain;
using ShopManagement.Domain.ProductAgg;
using Xunit;

namespace ShopManagement.Tests.AggregateTests;

public class ProductTests
{
    [Fact]
    public void DefaultConstructor_InheritsFromEntityBase()
    {
        var product = new Product();

        product.Should().BeAssignableTo<EntityBase>();
    }

    [Fact]
    public void DefaultConstructor_SetsCreationDate()
    {
        var beforeCreation = DateTime.UtcNow;

        var product = new Product();

        product.CreationDate.Should().BeOnOrAfter(beforeCreation);
        product.CreationDate.Should().BeOnOrBefore(DateTime.UtcNow);
    }

    [Fact]
    public void DefaultConstructor_SetsDefaultId()
    {
        var product = new Product();

        product.Id.Should().Be(0);
    }

    [Fact]
    public void ParameterizedConstructor_SetsName()
    {
        CreateProduct().Name.Should().Be("Product name");
    }

    [Fact]
    public void ParameterizedConstructor_SetsCode()
    {
        CreateProduct().Code.Should().Be("PRD-001");
    }

    [Fact]
    public void ParameterizedConstructor_SetsShortDescription()
    {
        CreateProduct().ShortDescription.Should().Be("Short description");
    }

    [Fact]
    public void ParameterizedConstructor_SetsDescription()
    {
        CreateProduct().Description.Should().Be("Description");
    }

    [Fact]
    public void ParameterizedConstructor_SetsPictureUrl()
    {
        CreateProduct().PictureUrl.Should().Be("product.jpg");
    }

    [Fact]
    public void ParameterizedConstructor_SetsPictureTitle()
    {
        CreateProduct().PictureTitle.Should().Be("Picture title");
    }

    [Fact]
    public void ParameterizedConstructor_SetsPictureAlt()
    {
        CreateProduct().PictureAlt.Should().Be("Picture alt");
    }

    [Fact]
    public void ParameterizedConstructor_SetsSlug()
    {
        CreateProduct().Slug.Should().Be("product-name");
    }

    [Fact]
    public void ParameterizedConstructor_SetsKeywords()
    {
        CreateProduct().Keywords.Should().Be("product, lamp");
    }

    [Fact]
    public void ParameterizedConstructor_SetsMetaDescription()
    {
        CreateProduct().MetaDescription.Should().Be("Product meta description");
    }

    [Fact]
    public void ParameterizedConstructor_SetsCategoryId()
    {
        CreateProduct().CategoryId.Should().Be(1);
    }

    [Fact]
    public void DefaultConstructor_LeavesCategoryUninitialized()
    {
        new Product().Category.Should().BeNull();
    }

    [Fact]
    public void DefaultConstructor_LeavesProductPicturesUninitialized()
    {
        new Product().ProductPictures.Should().BeNull();
    }

    [Fact]
    public void ClearPicture_ClearsPictureUrl()
    {
        var product = CreateProduct();

        product.ClearPicture();

        product.PictureUrl.Should().BeEmpty();
    }

    [Fact]
    public void Edit_WithPictureUrl_UpdatesAllEditableProductProperties()
    {
        var product = CreateProduct();

        product.Edit(
            "Updated name", "PRD-002", "Updated short description", "Updated description", "updated.jpg",
            "Updated title", "Updated alt", "updated-product", "updated, lamp", "Updated meta description", 2);

        product.Name.Should().Be("Updated name");
        product.Code.Should().Be("PRD-002");
        product.ShortDescription.Should().Be("Updated short description");
        product.Description.Should().Be("Updated description");
        product.PictureUrl.Should().Be("updated.jpg");
        product.PictureTitle.Should().Be("Updated title");
        product.PictureAlt.Should().Be("Updated alt");
        product.Slug.Should().Be("updated-product");
        product.Keywords.Should().Be("updated, lamp");
        product.MetaDescription.Should().Be("Updated meta description");
        product.CategoryId.Should().Be(2);
    }

    [Fact]
    public void Edit_WithWhitespacePictureUrl_PreservesCurrentProductPicture()
    {
        var product = CreateProduct();

        product.Edit(
            "Updated name", "PRD-002", "Updated short description", "Updated description", "   ",
            "Updated title", "Updated alt", "updated-product", "updated, lamp", "Updated meta description", 2);

        product.PictureUrl.Should().Be("product.jpg");
    }

    private static Product CreateProduct() => new(
        "Product name", "PRD-001", "Short description", "Description", "product.jpg", "Picture title", "Picture alt",
        "product-name", "product, lamp", "Product meta description", 1);
}