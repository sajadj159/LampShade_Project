using MediatR;
using _01_LampShadeQuery.Contract.ArticleCategory;

namespace _01_LampShadeQuery.Features.ArticleCategories.Queries.GetArticleCategoryBySlug;

public class GetArticleCategoryBySlugQuery : IRequest<ArticleCategoryQueryModel>
{
    public string Slug { get; set; } = string.Empty;
}

public class GetArticleCategoryBySlugQueryHandler : IRequestHandler<GetArticleCategoryBySlugQuery, ArticleCategoryQueryModel>
{
    private readonly IArticleCategoryQuery _query;
    public GetArticleCategoryBySlugQueryHandler(IArticleCategoryQuery query) => _query = query;
    public Task<ArticleCategoryQueryModel> Handle(GetArticleCategoryBySlugQuery r, CancellationToken c) => Task.FromResult(_query.GetArticleCategory(r.Slug));
}
