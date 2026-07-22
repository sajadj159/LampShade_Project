using MediatR;
using LampShade.ReadModel.Contracts.Article;

namespace LampShade.ReadModel.Contracts.Queries.Articles.GetArticleBySlug;

public class GetArticleBySlugQuery : IRequest<ArticleQueryModel>
{
    public string Slug { get; set; } = string.Empty;
}
