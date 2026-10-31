using System.Threading;
using System.Threading.Tasks;
using MediatR;
using BlogManagement.Application.ReadModel.Article;

using BlogManagement.Application.ReadModel.Queries.Articles.GetArticleBySlug;

namespace BlogManagement.Application.ReadModel.Features.Articles.Queries.GetArticleBySlug;



public class GetArticleBySlugQueryHandler : IRequestHandler<GetArticleBySlugQuery, ArticleQueryModel>
{
    private readonly IArticleQuery _query;
    public GetArticleBySlugQueryHandler(IArticleQuery query) => _query = query;
    public Task<ArticleQueryModel> Handle(GetArticleBySlugQuery r, CancellationToken c) => _query.GetArticleDetailsAsync(r.Slug, c);
}
