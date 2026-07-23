using System.Threading;
using System.Threading.Tasks;
using MediatR;
using LampShade.ReadModel.Contracts.Article;

using LampShade.ReadModel.Contracts.Queries.Articles.GetArticleBySlug;

namespace LampShade.ReadModel.Application.Features.Articles.Queries.GetArticleBySlug;



public class GetArticleBySlugQueryHandler : IRequestHandler<GetArticleBySlugQuery, ArticleQueryModel>
{
    private readonly IArticleQuery _query;
    public GetArticleBySlugQueryHandler(IArticleQuery query) => _query = query;
    public Task<ArticleQueryModel> Handle(GetArticleBySlugQuery r, CancellationToken c) => Task.FromResult(_query.GetArticleDetails(r.Slug));
}
