using System.Threading;
using System.Threading.Tasks;
using BlogManagement.Application.ReadModel.ArticleCategories.Dto;
using BlogManagement.Application.ReadModel.ArticleCategory;
using MediatR;

using BlogManagement.Application.ReadModel.Queries.ArticleCategories.GetArticleCategories;

namespace BlogManagement.Application.ReadModel.Features.ArticleCategories.Queries.GetArticleCategories;



public class GetArticleCategoriesQueryHandler : IRequestHandler<GetArticleCategoriesQuery, List<ArticleCategoryViewModel>>
{
    private readonly IArticleCategoryQuery _query;
    public GetArticleCategoriesQueryHandler(IArticleCategoryQuery query) => _query = query;
    public Task<List<ArticleCategoryViewModel>> Handle(GetArticleCategoriesQuery r, CancellationToken c) => _query.GetArticleCategoriesForManagementAsync(c);
}
