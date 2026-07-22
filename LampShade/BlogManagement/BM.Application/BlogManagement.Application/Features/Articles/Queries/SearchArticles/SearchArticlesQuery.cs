#nullable enable

using BlogManagement.Application.Contract.AC.Article;
using MediatR;

namespace BlogManagement.Application.Features.Articles.Queries.SearchArticles;

public class SearchArticlesQuery : IRequest<List<ArticleViewModel>>
{
    public string? Title { get; set; }
    public long? CategoryId { get; set; }
}

public class SearchArticlesQueryHandler : IRequestHandler<SearchArticlesQuery, List<ArticleViewModel>>
{
    private readonly IArticleApplication _articleApplication;

    public SearchArticlesQueryHandler(IArticleApplication articleApplication)
    {
        _articleApplication = articleApplication;
    }

    public Task<List<ArticleViewModel>> Handle(SearchArticlesQuery request, CancellationToken cancellationToken)
    {
        var searchModel = new ArticleSearchModel
        {
            Title = request.Title,
            CategoryId = request.CategoryId ?? 0
        };
        return Task.FromResult(_articleApplication.Search(searchModel));
    }
}

