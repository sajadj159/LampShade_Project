using System.Threading;
using System.Threading.Tasks;
using MediatR;
using BlogManagement.Application.ReadModel.ArticleCategory;

using BlogManagement.Application.ReadModel.Queries.ArticleCategories.GetArticleCategoryBySlug;

namespace BlogManagement.Application.ReadModel.Features.ArticleCategories.Queries.GetArticleCategoryBySlug;



public class GetArticleCategoryBySlugQueryHandler : IRequestHandler<GetArticleCategoryBySlugQuery, ArticleCategoryQueryModel>
{
    private readonly IArticleCategoryQuery _query;
    public GetArticleCategoryBySlugQueryHandler(IArticleCategoryQuery query) => _query = query;
    public Task<ArticleCategoryQueryModel> Handle(GetArticleCategoryBySlugQuery r, CancellationToken c) => _query.GetArticleCategoryAsync(r.Slug, c);
}
