using System.Threading;
using System.Threading.Tasks;
#nullable enable

using BlogManagement.Application.ReadModel.ArticleCategories.Dto;
using BlogManagement.Application.ReadModel.ArticleCategory;
using MediatR;

using BlogManagement.Application.ReadModel.Queries.ArticleCategories.SearchArticleCategories;

namespace BlogManagement.Application.ReadModel.Features.ArticleCategories.Queries.SearchArticleCategories;



public class SearchArticleCategoriesQueryHandler : IRequestHandler<SearchArticleCategoriesQuery, List<ArticleCategoryViewModel>>
{
    private readonly IArticleCategoryQuery _query;
    public SearchArticleCategoriesQueryHandler(IArticleCategoryQuery query) => _query = query;

    public Task<List<ArticleCategoryViewModel>> Handle(SearchArticleCategoriesQuery request, CancellationToken cancellationToken)
    {
        return _query.SearchArticleCategoriesAsync(request.Name, cancellationToken);
    }
}
