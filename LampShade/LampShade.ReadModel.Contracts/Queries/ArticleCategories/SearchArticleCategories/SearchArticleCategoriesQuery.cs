#nullable enable

using BlogManagement.Application.Contract.AC.ArticleCategory;
using MediatR;

namespace LampShade.ReadModel.Contracts.Queries.ArticleCategories.SearchArticleCategories;

public class SearchArticleCategoriesQuery : IRequest<List<ArticleCategoryViewModel>>
{
    public string? Name { get; set; }
}
