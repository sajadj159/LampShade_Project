using BlogManagement.Application.Contract.AC.Article;
using BlogManagement.Application.ReadModel.Article;
using BlogManagement.Infrastructure.EFCore;
using CommentManagement.Contracts;
using LampShade.BuildingBlocks.Application;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.ReadModel;

public class ArticleQuery(BlogContext context, ICommentModuleApi comments) : IArticleQuery
{
    public async Task<ArticleQueryModel> GetArticleDetailsAsync(string value, CancellationToken cancellationToken = default)
    {
        var result = await context.Articles.AsNoTracking().Where(x => x.PublishDate <= DateTime.UtcNow && x.Slug == value)
            .Select(x => new ArticleQueryModel { Id = x.Id, Title = x.Title, ShortDescription = x.ShortDescription, Description = x.Description, PictureUrl = x.PictureUrl, PictureTitle = x.PictureTitle, PictureAlt = x.PictureAlt, PublishDate = x.PublishDate.ToFarsi(), Slug = x.Slug, Keywords = x.Keywords, MetaDescription = x.MetaDescription, CanonicalAddress = x.CanonicalAddress, CategoryName = x.Category.Name, CategorySlug = x.Category.Slug })
            .FirstOrDefaultAsync(cancellationToken);
        if (result is null) return new ArticleQueryModel();
        if (!string.IsNullOrWhiteSpace(result.Keywords)) result.KeywordsList = result.Keywords.Split(',').ToList();
        result.Comments = await comments.GetConfirmedCommentsAsync(CommentOwnerType.Article, result.Id, cancellationToken);
        return result;
    }

    public Task<List<ArticleQueryModel>> LatestArticlesAsync(CancellationToken cancellationToken = default) =>
        context.Articles.AsNoTracking().Where(x => x.PublishDate <= DateTime.UtcNow).Select(x => new ArticleQueryModel { Id = x.Id, Title = x.Title, ShortDescription = x.ShortDescription, PictureUrl = x.PictureUrl, PictureTitle = x.PictureTitle, PictureAlt = x.PictureAlt, PublishDate = x.PublishDate.ToFarsi(), Slug = x.Slug }).ToListAsync(cancellationToken);

    public Task<List<ArticleViewModel>> SearchAsync(ArticleSearchModel searchModel, CancellationToken cancellationToken = default)
    {
        var queryable = context.Articles.AsNoTracking().Select(x => new ArticleViewModel
        {
            Id = x.Id,
            CreatedDate = x.CreationDate.ToFarsi(),
            PictureUrl = x.PictureUrl,
            Title = x.Title,
            ShortDescription = x.ShortDescription.Substring(0, Math.Min(x.ShortDescription.Length, 50)) + "...",
            PublishDate = x.PublishDate.ToFarsi(),
            Category = x.Category.Name,
            CategoryId = x.CategoryId
        });
        if (searchModel.CategoryId > 0)
            queryable = queryable.Where(x => x.CategoryId == searchModel.CategoryId);
        if (!string.IsNullOrWhiteSpace(searchModel.Title))
            queryable = queryable.Where(x => x.Title.Contains(searchModel.Title));
        return queryable.OrderByDescending(x => x.Id).ToListAsync(cancellationToken);
    }

    public Task<EditArticle> GetDetailsAsync(long id, CancellationToken cancellationToken = default) =>
        context.Articles.AsNoTracking().Where(x => x.Id == id).Select(x => new EditArticle
        {
            Title = x.Title,
            ShortDescription = x.ShortDescription,
            Description = x.Description,
            PictureTitle = x.PictureTitle,
            PictureAlt = x.PictureAlt,
            PublishDate = x.PublishDate.ToFarsi(),
            Slug = x.Slug,
            Keywords = x.Keywords,
            MetaDescription = x.MetaDescription,
            CanonicalAddress = x.CanonicalAddress,
            CategoryId = x.CategoryId,
            Id = x.Id
        }).FirstOrDefaultAsync(cancellationToken);
}
