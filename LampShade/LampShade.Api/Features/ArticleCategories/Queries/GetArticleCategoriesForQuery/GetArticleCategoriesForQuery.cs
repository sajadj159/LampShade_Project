using MediatR;
using _01_LampShadeQuery.Contract.ArticleCategory;

namespace LampShade.Api.Features.ArticleCategories.Queries.GetArticleCategoriesForQuery;

public class GetArticleCategoriesForQuery : IRequest<List<ArticleCategoryQueryModel>> { }

public class GetArticleCategoriesForQueryHandler : IRequestHandler<GetArticleCategoriesForQuery, List<ArticleCategoryQueryModel>>
{
    private readonly IArticleCategoryQuery _query;
    public GetArticleCategoriesForQueryHandler(IArticleCategoryQuery query) => _query = query;
    public async Task<List<ArticleCategoryQueryModel>> Handle(GetArticleCategoriesForQuery r, CancellationToken c) => await Task.FromResult(_query.GetArticleCategories());
}
