using AwesomeAssertions;
using _0_Framework.Domain;
using ShopManagement.Domain.ProductCategoryAgg;
using Xunit;

namespace ShopManagement.Tests.AggregateTests;

public class ProductCategoryTests
{
    [Fact] public void DefaultConstructor_InheritsFromEntityBase() => new ProductCategory().Should().BeAssignableTo<EntityBase>();
    [Fact] public void DefaultConstructor_SetsCreationDate() { var beforeCreation = DateTime.UtcNow; var category = new ProductCategory(); category.CreationDate.Should().BeOnOrAfter(beforeCreation); category.CreationDate.Should().BeOnOrBefore(DateTime.UtcNow); }
    [Fact] public void DefaultConstructor_SetsDefaultId() => new ProductCategory().Id.Should().Be(0);
    [Fact] public void DefaultConstructor_InitializesProducts() { var category = new ProductCategory(); category.Products.Should().NotBeNull(); category.Products.Should().BeEmpty(); }
    [Fact] public void ParameterizedConstructor_SetsName() => CreateCategory().Name.Should().Be("Category name");
    [Fact] public void ParameterizedConstructor_SetsDescription() => CreateCategory().Description.Should().Be("Category description");
    [Fact] public void ParameterizedConstructor_SetsPictureUrl() => CreateCategory().PictureUrl.Should().Be("category.jpg");
    [Fact] public void ParameterizedConstructor_SetsPictureAlt() => CreateCategory().PictureAlt.Should().Be("Category alt");
    [Fact] public void ParameterizedConstructor_SetsPictureTitle() => CreateCategory().PictureTitle.Should().Be("Category title");
    [Fact] public void ParameterizedConstructor_SetsKeywords() => CreateCategory().Keywords.Should().Be("category, lamp");
    [Fact] public void ParameterizedConstructor_SetsMetaDescription() => CreateCategory().MetaDescription.Should().Be("Category meta description");
    [Fact] public void ParameterizedConstructor_SetsSlug() => CreateCategory().Slug.Should().Be("category-name");
    [Fact]
    public void Edit_WithPictureUrl_UpdatesAllEditableProperties()
    {
        var category = CreateCategory();
        category.Edit("Updated category", "Updated description", "updated.jpg", "Updated alt", "Updated title", "updated, category", "Updated meta description", "updated-category");
        category.Name.Should().Be("Updated category"); category.Description.Should().Be("Updated description"); category.PictureUrl.Should().Be("updated.jpg"); category.PictureAlt.Should().Be("Updated alt"); category.PictureTitle.Should().Be("Updated title"); category.Keywords.Should().Be("updated, category"); category.MetaDescription.Should().Be("Updated meta description"); category.Slug.Should().Be("updated-category");
    }
    [Fact]
    public void Edit_WithWhitespacePictureUrl_PreservesCurrentPicture()
    {
        var category = CreateCategory();
        category.Edit("Updated category", "Updated description", " ", "Updated alt", "Updated title", "updated, category", "Updated meta description", "updated-category");
        category.PictureUrl.Should().Be("category.jpg");
    }
    private static ProductCategory CreateCategory() => new("Category name", "Category description", "category.jpg", "Category alt", "Category title", "category, lamp", "Category meta description", "category-name");
}