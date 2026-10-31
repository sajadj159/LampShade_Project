#nullable enable

using BlogManagement.Application.Contract.AC.Article;
using MediatR;

namespace BlogManagement.Application.ReadModel.Queries.Articles.SearchArticles;

public class SearchArticlesQuery : IRequest<List<ArticleViewModel>>
{
    public string? Title { get; set; }
    public long? CategoryId { get; set; }
}
