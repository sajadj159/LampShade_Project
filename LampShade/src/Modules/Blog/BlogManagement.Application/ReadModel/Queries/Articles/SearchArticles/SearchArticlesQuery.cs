#nullable enable

using BlogManagement.Application.Contract.AC.Article;
using MediatR;

namespace LampShade.ReadModel.Contracts.Queries.Articles.SearchArticles;

public class SearchArticlesQuery : IRequest<List<ArticleViewModel>>
{
    public string? Title { get; set; }
    public long? CategoryId { get; set; }
}
