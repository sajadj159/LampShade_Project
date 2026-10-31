using MediatR;
using BlogManagement.Application.ReadModel.Article;

namespace BlogManagement.Application.ReadModel.Queries.Articles.GetArticleBySlug;

public class GetArticleBySlugQuery : IRequest<ArticleQueryModel>
{
    public string Slug { get; set; } = string.Empty;
}
