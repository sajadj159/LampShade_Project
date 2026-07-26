using System.Threading;
using System.Threading.Tasks;
using LampShade.ReadModel.Contracts.ArticleCategories.Dto;
using LampShade.ReadModel.Contracts.ArticleCategory;
using MediatR;

using LampShade.ReadModel.Contracts.Queries.ArticleCategories.GetArticleCategories;

namespace LampShade.ReadModel.Application.Features.ArticleCategories.Queries.GetArticleCategories;



public class GetArticleCategoriesQueryHandler : IRequestHandler<GetArticleCategoriesQuery, List<ArticleCategoryViewModel>>
{
    private readonly IArticleCategoryQuery _query;
    public GetArticleCategoriesQueryHandler(IArticleCategoryQuery query) => _query = query;
    public Task<List<ArticleCategoryViewModel>> Handle(GetArticleCategoriesQuery r, CancellationToken c) => _query.GetArticleCategoriesForManagementAsync(c);
}
