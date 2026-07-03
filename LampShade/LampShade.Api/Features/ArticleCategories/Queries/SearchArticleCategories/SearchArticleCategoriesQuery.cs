using BlogManagement.Application.Contract.AC.ArticleCategory;
using MediatR;

namespace LampShade.Api.Features.ArticleCategories.Queries.SearchArticleCategories;

public class SearchArticleCategoriesQuery : IRequest<List<ArticleCategoryViewModel>>
{
    public string? Name { get; set; }
}

public class SearchArticleCategoriesQueryHandler : IRequestHandler<SearchArticleCategoriesQuery, List<ArticleCategoryViewModel>>
{
    private readonly IArticleCategoryApplication _application;
    public SearchArticleCategoriesQueryHandler(IArticleCategoryApplication application) => _application = application;

    public async Task<List<ArticleCategoryViewModel>> Handle(SearchArticleCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_application.Search(new ArticleCategorySearchModel { Name = request.Name }));
    }
}
