using MediatR;
using LampShade.ReadModel.Contracts.ArticleCategory;

namespace LampShade.ReadModel.Contracts.Queries.ArticleCategories.GetArticleCategoryBySlug;

public class GetArticleCategoryBySlugQuery : IRequest<ArticleCategoryQueryModel>
{
    public string Slug { get; set; } = string.Empty;
}
