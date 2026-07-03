using MediatR;
using _01_LampShadeQuery.Contract.Article;

namespace LampShade.Api.Features.Articles.Queries.LatestArticles;

public class LatestArticlesQuery : IRequest<List<ArticleQueryModel>> { }

public class LatestArticlesQueryHandler : IRequestHandler<LatestArticlesQuery, List<ArticleQueryModel>>
{
    private readonly IArticleQuery _query;
    public LatestArticlesQueryHandler(IArticleQuery query) => _query = query;
    public async Task<List<ArticleQueryModel>> Handle(LatestArticlesQuery r, CancellationToken c) => await Task.FromResult(_query.LatestArticles());
}
