#nullable enable

using BlogManagement.Application.Contract.AC.ArticleCategory;
using MediatR;

namespace BlogManagement.Application.Features.ArticleCategories.Queries.SearchArticleCategories;

public class SearchArticleCategoriesQuery : IRequest<List<ArticleCategoryViewModel>>
{
    public string? Name { get; set; }
}

public class SearchArticleCategoriesQueryHandler : IRequestHandler<SearchArticleCategoriesQuery, List<ArticleCategoryViewModel>>
{
    private readonly IArticleCategoryApplication _application;
    public SearchArticleCategoriesQueryHandler(IArticleCategoryApplication application) => _application = application;

    public Task<List<ArticleCategoryViewModel>> Handle(SearchArticleCategoriesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_application.Search(new ArticleCategorySearchModel { Name = request.Name }));
    }
}
