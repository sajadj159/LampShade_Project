using MediatR;
using _01_LampShadeQuery.Contract.ArticleCategory;

namespace LampShade.Api.Features.ArticleCategories.Queries.GetArticleCategoryBySlug;

public class GetArticleCategoryBySlugQuery : IRequest<ArticleCategoryQueryModel>
{
    public string Slug { get; set; } = string.Empty;
}

public class GetArticleCategoryBySlugQueryHandler : IRequestHandler<GetArticleCategoryBySlugQuery, ArticleCategoryQueryModel>
{
    private readonly IArticleCategoryQuery _query;
    public GetArticleCategoryBySlugQueryHandler(IArticleCategoryQuery query) => _query = query;
    public async Task<ArticleCategoryQueryModel> Handle(GetArticleCategoryBySlugQuery r, CancellationToken c) => await Task.FromResult(_query.GetArticleCategory(r.Slug));
}
