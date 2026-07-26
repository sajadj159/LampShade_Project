using AwesomeAssertions;
using _0_Framework.Domain;
using BlogManagement.Domain.ArticleCategoryAgg;
using Xunit;

namespace BlogManagement.Tests.AggregateTests;

public class ArticleCategoryTests
{
    [Fact] public void Constructor_InheritsFromEntityBase() => CreateCategory().Should().BeAssignableTo<EntityBase>();
    [Fact] public void Constructor_SetsCreationDate() { var beforeCreation = DateTime.UtcNow; var category = CreateCategory(); category.CreationDate.Should().BeOnOrAfter(beforeCreation); category.CreationDate.Should().BeOnOrBefore(DateTime.UtcNow); }
    [Fact] public void Constructor_SetsDefaultId() => CreateCategory().Id.Should().Be(0);
    [Fact] public void Constructor_SetsName() => CreateCategory().Name.Should().Be("Category name");
    [Fact] public void Constructor_SetsPictureUrl() => CreateCategory().PictureUrl.Should().Be("category.jpg");
    [Fact] public void Constructor_SetsPictureAlt() => CreateCategory().PictureAlt.Should().Be("Category alt");
    [Fact] public void Constructor_SetsPictureTitle() => CreateCategory().PictureTitle.Should().Be("Category picture title");
    [Fact] public void Constructor_SetsDescription() => CreateCategory().Description.Should().Be("Category description");
    [Fact] public void Constructor_SetsShowOrder() => CreateCategory().ShowOrder.Should().Be(1);
    [Fact] public void Constructor_SetsSlug() => CreateCategory().Slug.Should().Be("category-name");
    [Fact] public void Constructor_SetsKeywords() => CreateCategory().Keywords.Should().Be("category, blog");
    [Fact] public void Constructor_SetsMetaDescription() => CreateCategory().MetaDescription.Should().Be("Category meta description");
    [Fact] public void Constructor_SetsCanonicalAddress() => CreateCategory().CanonicalAddress.Should().Be("https://example.com/categories/category-name");
    [Fact] public void Constructor_LeavesArticlesUninitialized() => CreateCategory().Articles.Should().BeNull();
    [Fact]
    public void Edit_WithPictureUrl_UpdatesAllEditableProperties()
    {
        var category = CreateCategory(); category.Edit("Updated category", "updated.jpg", "Updated alt", "Updated title", "Updated description", 2, "updated-category", "updated, blog", "Updated meta description", "https://example.com/categories/updated-category");
        category.Name.Should().Be("Updated category"); category.PictureUrl.Should().Be("updated.jpg"); category.PictureAlt.Should().Be("Updated alt"); category.PictureTitle.Should().Be("Updated title"); category.Description.Should().Be("Updated description"); category.ShowOrder.Should().Be(2); category.Slug.Should().Be("updated-category"); category.Keywords.Should().Be("updated, blog"); category.MetaDescription.Should().Be("Updated meta description"); category.CanonicalAddress.Should().Be("https://example.com/categories/updated-category");
    }
    [Fact] public void Edit_WithWhitespacePictureUrl_PreservesCurrentPicture() { var category = CreateCategory(); category.Edit("Updated category", " ", "Updated alt", "Updated title", "Updated description", 2, "updated-category", "updated, blog", "Updated meta description", "https://example.com/categories/updated-category"); category.PictureUrl.Should().Be("category.jpg"); }
    private static ArticleCategory CreateCategory() => new("Category name", "category.jpg", "Category alt", "Category picture title", "Category description", 1, "category-name", "category, blog", "Category meta description", "https://example.com/categories/category-name");
}