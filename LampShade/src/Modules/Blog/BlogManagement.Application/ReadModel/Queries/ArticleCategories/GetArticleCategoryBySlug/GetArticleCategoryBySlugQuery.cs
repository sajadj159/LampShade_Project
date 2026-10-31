using MediatR;
using BlogManagement.Application.ReadModel.ArticleCategory;

namespace BlogManagement.Application.ReadModel.Queries.ArticleCategories.GetArticleCategoryBySlug;

public class GetArticleCategoryBySlugQuery : IRequest<ArticleCategoryQueryModel>
{
    public string Slug { get; set; } = string.Empty;
}
