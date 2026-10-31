using _0_Framework.Application;
using BlogManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore;
using LampShade.ReadModel.Contracts.Article;
using LampShade.ReadModel.Contracts.Comment;
using Microsoft.EntityFrameworkCore;

namespace LampShade.ReadModel.Application.Query;

public class ArticleQuery(BlogContext context, CommentContext commentContext) : IArticleQuery
{
    public async Task<ArticleQueryModel> GetArticleDetailsAsync(string value, CancellationToken cancellationToken = default)
    {
        var result = await context.Articles.AsNoTracking().Where(x => x.PublishDate <= DateTime.UtcNow && x.Slug == value)
            .Select(x => new ArticleQueryModel { Id = x.Id, Title = x.Title, ShortDescription = x.ShortDescription, Description = x.Description, PictureUrl = x.PictureUrl, PictureTitle = x.PictureTitle, PictureAlt = x.PictureAlt, PublishDate = x.PublishDate.ToFarsi(), Slug = x.Slug, Keywords = x.Keywords, MetaDescription = x.MetaDescription, CanonicalAddress = x.CanonicalAddress, CategoryName = x.Category.Name, CategorySlug = x.Category.Slug })
            .FirstOrDefaultAsync(cancellationToken);
        if (result is null) return new ArticleQueryModel();
        if (!string.IsNullOrWhiteSpace(result.Keywords)) result.KeywordsList = result.Keywords.Split(',').ToList();
        var comments = await commentContext.Comments.AsNoTracking().Where(x => x.OwnerRecordId == result.Id && !x.IsCanceled && x.IsConfirmed && x.Type == CommentType.Article)
            .Select(x => new CommentQueryModel { Id = x.Id, Name = x.Name, Description = x.Description, ParentId = x.ParentId, CreationDate = x.CreationDate.ToFarsi() }).OrderByDescending(x => x.Id).ToListAsync(cancellationToken);
        foreach (var comment in comments.Where(x => x.ParentId > 0)) comment.ParentName = comments.FirstOrDefault(x => x.Id == comment.ParentId)?.Name;
        result.Comments = comments;
        return result;
    }

    public Task<List<ArticleQueryModel>> LatestArticlesAsync(CancellationToken cancellationToken = default) =>
        context.Articles.AsNoTracking().Where(x => x.PublishDate <= DateTime.UtcNow).Select(x => new ArticleQueryModel { Id = x.Id, Title = x.Title, ShortDescription = x.ShortDescription, PictureUrl = x.PictureUrl, PictureTitle = x.PictureTitle, PictureAlt = x.PictureAlt, PublishDate = x.PublishDate.ToFarsi(), Slug = x.Slug }).ToListAsync(cancellationToken);
}