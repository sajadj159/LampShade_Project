using MediatR;
using _01_LampShadeQuery.Contract.Article;

namespace LampShade.Api.Features.Articles.Queries.GetArticleBySlug;

public class GetArticleBySlugQuery : IRequest<ArticleQueryModel>
{
    public string Slug { get; set; } = string.Empty;
}

public class GetArticleBySlugQueryHandler : IRequestHandler<GetArticleBySlugQuery, ArticleQueryModel>
{
    private readonly IArticleQuery _query;
    public GetArticleBySlugQueryHandler(IArticleQuery query) => _query = query;
    public async Task<ArticleQueryModel> Handle(GetArticleBySlugQuery r, CancellationToken c) => await Task.FromResult(_query.GetArticleDetails(r.Slug));
}
