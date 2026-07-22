#nullable enable

using BlogManagement.Application.Contract.AC.ArticleCategory;
using MediatR;

using LampShade.ReadModel.Contracts.Queries.ArticleCategories.SearchArticleCategories;

namespace LampShade.ReadModel.Application.Features.ArticleCategories.Queries.SearchArticleCategories;



public class SearchArticleCategoriesQueryHandler : IRequestHandler<SearchArticleCategoriesQuery, List<ArticleCategoryViewModel>>
{
    private readonly IArticleCategoryApplication _application;
    public SearchArticleCategoriesQueryHandler(IArticleCategoryApplication application) => _application = application;

    public Task<List<ArticleCategoryViewModel>> Handle(SearchArticleCategoriesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_application.Search(new ArticleCategorySearchModel { Name = request.Name }));
    }
}
