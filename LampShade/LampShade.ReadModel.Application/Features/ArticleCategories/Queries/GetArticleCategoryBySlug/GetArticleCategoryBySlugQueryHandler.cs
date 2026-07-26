using System.Threading;
using System.Threading.Tasks;
using MediatR;
using LampShade.ReadModel.Contracts.ArticleCategory;

using LampShade.ReadModel.Contracts.Queries.ArticleCategories.GetArticleCategoryBySlug;

namespace LampShade.ReadModel.Application.Features.ArticleCategories.Queries.GetArticleCategoryBySlug;



public class GetArticleCategoryBySlugQueryHandler : IRequestHandler<GetArticleCategoryBySlugQuery, ArticleCategoryQueryModel>
{
    private readonly IArticleCategoryQuery _query;
    public GetArticleCategoryBySlugQueryHandler(IArticleCategoryQuery query) => _query = query;
    public Task<ArticleCategoryQueryModel> Handle(GetArticleCategoryBySlugQuery r, CancellationToken c) => _query.GetArticleCategoryAsync(r.Slug, c);
}
