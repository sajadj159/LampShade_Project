using BlogManagement.Application.Contract.AC.Article;
using BlogManagement.Application.ReadModel.Article;
using BlogManagement.Application.ReadModel.Queries.Articles.SearchArticles;
using MediatR;

namespace BlogManagement.Application.ReadModel.Features.Articles.Queries.SearchArticles;

public class SearchArticlesQueryHandler(IArticleQuery query) : IRequestHandler<SearchArticlesQuery, List<ArticleViewModel>>
{
    public Task<List<ArticleViewModel>> Handle(SearchArticlesQuery request, CancellationToken cancellationToken) =>
        query.SearchAsync(new ArticleSearchModel { Title = request.Title, CategoryId = request.CategoryId ?? 0 }, cancellationToken);
}
