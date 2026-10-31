using System.Threading;
using System.Threading.Tasks;
using MediatR;
using BlogManagement.Application.ReadModel.Article;

using BlogManagement.Application.ReadModel.Queries.Articles.LatestArticles;

namespace BlogManagement.Application.ReadModel.Features.Articles.Queries.LatestArticles;



public class LatestArticlesQueryHandler : IRequestHandler<LatestArticlesQuery, List<ArticleQueryModel>>
{
    private readonly IArticleQuery _query;
    public LatestArticlesQueryHandler(IArticleQuery query) => _query = query;
    public Task<List<ArticleQueryModel>> Handle(LatestArticlesQuery r, CancellationToken c) => _query.LatestArticlesAsync(c);
}
