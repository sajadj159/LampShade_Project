#nullable enable

using BlogManagement.Application.ReadModel.ArticleCategories.Dto;
using MediatR;

namespace BlogManagement.Application.ReadModel.Queries.ArticleCategories.SearchArticleCategories;

public class SearchArticleCategoriesQuery : IRequest<List<ArticleCategoryViewModel>>
{
    public string? Name { get; set; }
}

