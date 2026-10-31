#nullable enable

using LampShade.ReadModel.Contracts.ArticleCategories.Dto;
using MediatR;

namespace LampShade.ReadModel.Contracts.Queries.ArticleCategories.SearchArticleCategories;

public class SearchArticleCategoriesQuery : IRequest<List<ArticleCategoryViewModel>>
{
    public string? Name { get; set; }
}

