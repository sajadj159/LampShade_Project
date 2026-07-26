using AwesomeAssertions;
using _0_Framework.Domain;
using BlogManagement.Domain.ArticleAgg;
using Xunit;

namespace BlogManagement.Tests.AggregateTests;

public class ArticleTests
{
    [Fact] public void Constructor_InheritsFromEntityBase() => CreateArticle().Should().BeAssignableTo<EntityBase>();
    [Fact] public void Constructor_SetsCreationDate() { var beforeCreation = DateTime.UtcNow; var article = CreateArticle(); article.CreationDate.Should().BeOnOrAfter(beforeCreation); article.CreationDate.Should().BeOnOrBefore(DateTime.UtcNow); }
    [Fact] public void Constructor_SetsDefaultId() => CreateArticle().Id.Should().Be(0);
    [Fact] public void Constructor_SetsTitle() => CreateArticle().Title.Should().Be("Article title");
    [Fact] public void Constructor_SetsShortDescription() => CreateArticle().ShortDescription.Should().Be("Short description");
    [Fact] public void Constructor_SetsDescription() => CreateArticle().Description.Should().Be("Article description");
    [Fact] public void Constructor_SetsPictureUrl() => CreateArticle().PictureUrl.Should().Be("article.jpg");
    [Fact] public void Constructor_SetsPictureTitle() => CreateArticle().PictureTitle.Should().Be("Article title image");
    [Fact] public void Constructor_SetsPictureAlt() => CreateArticle().PictureAlt.Should().Be("Article alt image");
    [Fact] public void Constructor_SetsPublishDate() => CreateArticle().PublishDate.Should().Be(PublishDate);
    [Fact] public void Constructor_SetsSlug() => CreateArticle().Slug.Should().Be("article-title");
    [Fact] public void Constructor_SetsKeywords() => CreateArticle().Keywords.Should().Be("article, blog");
    [Fact] public void Constructor_SetsMetaDescription() => CreateArticle().MetaDescription.Should().Be("Article meta description");
    [Fact] public void Constructor_SetsCanonicalAddress() => CreateArticle().CanonicalAddress.Should().Be("https://example.com/articles/article-title");
    [Fact] public void Constructor_SetsCategoryId() => CreateArticle().CategoryId.Should().Be(1);
    [Fact] public void Constructor_LeavesCategoryUninitialized() => CreateArticle().Category.Should().BeNull();
    [Fact]
    public void Edit_WithPictureUrl_UpdatesAllEditableProperties()
    {
        var article = CreateArticle(); article.Edit("Updated title", "Updated short description", "Updated description", "updated.jpg", "Updated picture title", "Updated picture alt", UpdatedPublishDate, "updated-title", "updated, blog", "Updated meta description", "https://example.com/articles/updated-title", 2);
        article.Title.Should().Be("Updated title"); article.ShortDescription.Should().Be("Updated short description"); article.Description.Should().Be("Updated description"); article.PictureUrl.Should().Be("updated.jpg"); article.PictureTitle.Should().Be("Updated picture title"); article.PictureAlt.Should().Be("Updated picture alt"); article.PublishDate.Should().Be(UpdatedPublishDate); article.Slug.Should().Be("updated-title"); article.Keywords.Should().Be("updated, blog"); article.MetaDescription.Should().Be("Updated meta description"); article.CanonicalAddress.Should().Be("https://example.com/articles/updated-title"); article.CategoryId.Should().Be(2);
    }
    [Fact] public void Edit_WithWhitespacePictureUrl_PreservesCurrentPicture() { var article = CreateArticle(); article.Edit("Updated title", "Updated short description", "Updated description", " ", "Updated picture title", "Updated picture alt", UpdatedPublishDate, "updated-title", "updated, blog", "Updated meta description", "https://example.com/articles/updated-title", 2); article.PictureUrl.Should().Be("article.jpg"); }
    private static readonly DateTime PublishDate = new(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    private static readonly DateTime UpdatedPublishDate = new(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc);
    private static Article CreateArticle() => new("Article title", "Short description", "Article description", "article.jpg", "Article title image", "Article alt image", PublishDate, "article-title", "article, blog", "Article meta description", "https://example.com/articles/article-title", 1);
}