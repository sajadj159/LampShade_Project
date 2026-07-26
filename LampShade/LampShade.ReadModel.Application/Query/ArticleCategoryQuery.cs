using _0_Framework.Application;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;
using BlogManagement.Infrastructure.EFCore;
using LampShade.ReadModel.Contracts.Article;
using LampShade.ReadModel.Contracts.ArticleCategory;
using LampShade.ReadModel.Contracts.ArticleCategories.Dto;
using Microsoft.EntityFrameworkCore;

namespace LampShade.ReadModel.Application.Query;

public class ArticleCategoryQuery(BlogContext context) : IArticleCategoryQuery
{
    public async Task<ArticleCategoryQueryModel> GetArticleCategoryAsync(string slug, CancellationToken cancellationToken = default)
    {
        var category = await context.ArticleCategories.AsNoTracking().Include(x => x.Articles).FirstOrDefaultAsync(x => x.Slug == slug, cancellationToken);
        if (category is null) return new ArticleCategoryQueryModel();
        var result = MapCategory(category, includeDetails: true);
        if (!string.IsNullOrWhiteSpace(result.Keywords)) result.KeywordList = result.Keywords.Split(',').ToList();
        return result;
    }

    public async Task<List<ArticleCategoryQueryModel>> GetArticleCategoriesAsync(CancellationToken cancellationToken = default)
    {
        var categories = await context.ArticleCategories.AsNoTracking().Include(x => x.Articles).ToListAsync(cancellationToken);
        return categories.Select(x => MapCategory(x, includeDetails: false)).ToList();
    }

    public Task<ArticleCategoryDetailsDto> GetArticleCategoryForManagementAsync(long id, CancellationToken cancellationToken = default) =>
        context.ArticleCategories.AsNoTracking().Where(x => x.Id == id).Select(x => new ArticleCategoryDetailsDto { Id = x.Id, Name = x.Name, PictureUrl = x.PictureUrl, PictureAlt = x.PictureAlt, PictureTitle = x.PictureTitle, Description = x.Description, ShowOrder = x.ShowOrder, Slug = x.Slug, Keywords = x.Keywords, MetaDescription = x.MetaDescription, CanonicalAddress = x.CanonicalAddress }).FirstOrDefaultAsync(cancellationToken);

    public Task<List<ArticleCategoryViewModel>> GetArticleCategoriesForManagementAsync(CancellationToken cancellationToken = default) =>
        ProjectArticleCategories(context.ArticleCategories.AsNoTracking()).ToListAsync(cancellationToken);

    public Task<List<ArticleCategoryViewModel>> SearchArticleCategoriesAsync(string name, CancellationToken cancellationToken = default)
    {
        var query = context.ArticleCategories.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(name)) query = query.Where(x => x.Name.Contains(name));
        return ProjectArticleCategories(query).ToListAsync(cancellationToken);
    }

    private static ArticleCategoryQueryModel MapCategory(ArticleCategory category, bool includeDetails) => new()
    {
        Name = category.Name, PictureUrl = category.PictureUrl, PictureAlt = category.PictureAlt, PictureTitle = category.PictureTitle, Slug = category.Slug, ArticlesCount = category.Articles.Count, Articles = MapArticles(category.Articles),
        Description = includeDetails ? category.Description : string.Empty, Keywords = includeDetails ? category.Keywords : string.Empty, MetaDescription = includeDetails ? category.MetaDescription : string.Empty, CanonicalAddress = includeDetails ? category.CanonicalAddress : string.Empty
    };

    private static List<ArticleQueryModel> MapArticles(IEnumerable<Article> articles) => articles.Select(x => new ArticleQueryModel { Id = x.Id, Title = x.Title, ShortDescription = x.ShortDescription, PictureUrl = x.PictureUrl, PictureTitle = x.PictureTitle, PictureAlt = x.PictureAlt, PublishDate = x.PublishDate.ToFarsi(), Slug = x.Slug }).ToList();

    private static IQueryable<ArticleCategoryViewModel> ProjectArticleCategories(IQueryable<ArticleCategory> query) => query.Select(x => new ArticleCategoryViewModel { Id = x.Id, Name = x.Name, Description = x.Description, PictureUrl = x.PictureUrl, ShowOrder = x.ShowOrder, CreationDate = x.CreationDate.ToFarsi(), ArticlesCount = x.Articles.Count });
}