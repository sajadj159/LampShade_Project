using MediatR;
using LampShade.ReadModel.Contracts.ArticleCategory;

using QueryRequest = LampShade.ReadModel.Contracts.Queries.ArticleCategories.GetArticleCategoriesForQuery.GetArticleCategoriesForQuery;

namespace LampShade.ReadModel.Application.Features.ArticleCategories.Queries.GetArticleCategoriesForQuery;



public class GetArticleCategoriesForQueryHandler : IRequestHandler<QueryRequest, List<ArticleCategoryQueryModel>>
{
    private readonly IArticleCategoryQuery _query;
    public GetArticleCategoriesForQueryHandler(IArticleCategoryQuery query) => _query = query;
    public Task<List<ArticleCategoryQueryModel>> Handle(QueryRequest r, CancellationToken c) => Task.FromResult(_query.GetArticleCategories());
}



