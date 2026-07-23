#nullable enable

using LampShade.ReadModel.Contracts.ArticleCategories.Dto;
using LampShade.ReadModel.Contracts.ArticleCategory;
using MediatR;

using LampShade.ReadModel.Contracts.Queries.ArticleCategories.SearchArticleCategories;

namespace LampShade.ReadModel.Application.Features.ArticleCategories.Queries.SearchArticleCategories;



public class SearchArticleCategoriesQueryHandler : IRequestHandler<SearchArticleCategoriesQuery, List<ArticleCategoryViewModel>>
{
    private readonly IArticleCategoryQuery _query;
    public SearchArticleCategoriesQueryHandler(IArticleCategoryQuery query) => _query = query;

    public Task<List<ArticleCategoryViewModel>> Handle(SearchArticleCategoriesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_query.SearchArticleCategories(request.Name));
    }
}

