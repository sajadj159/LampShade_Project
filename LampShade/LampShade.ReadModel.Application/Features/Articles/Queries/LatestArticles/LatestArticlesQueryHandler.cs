using MediatR;
using LampShade.ReadModel.Contracts.Article;

using LampShade.ReadModel.Contracts.Queries.Articles.LatestArticles;

namespace LampShade.ReadModel.Application.Features.Articles.Queries.LatestArticles;



public class LatestArticlesQueryHandler : IRequestHandler<LatestArticlesQuery, List<ArticleQueryModel>>
{
    private readonly IArticleQuery _query;
    public LatestArticlesQueryHandler(IArticleQuery query) => _query = query;
    public Task<List<ArticleQueryModel>> Handle(LatestArticlesQuery r, CancellationToken c) => Task.FromResult(_query.LatestArticles());
}
