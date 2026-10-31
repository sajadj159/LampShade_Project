using System.Threading;
using System.Threading.Tasks;
using MediatR;
using BlogManagement.Application.ReadModel.ArticleCategory;

using QueryRequest = BlogManagement.Application.ReadModel.Queries.ArticleCategories.GetArticleCategoriesForQuery.GetArticleCategoriesForQuery;

namespace BlogManagement.Application.ReadModel.Features.ArticleCategories.Queries.GetArticleCategoriesForQuery;



public class GetArticleCategoriesForQueryHandler : IRequestHandler<QueryRequest, List<ArticleCategoryQueryModel>>
{
    private readonly IArticleCategoryQuery _query;
    public GetArticleCategoriesForQueryHandler(IArticleCategoryQuery query) => _query = query;
    public Task<List<ArticleCategoryQueryModel>> Handle(QueryRequest r, CancellationToken c) => _query.GetArticleCategoriesAsync(c);
}
