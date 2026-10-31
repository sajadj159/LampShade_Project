using MediatR;
using BlogManagement.Application.ReadModel.Article;

namespace BlogManagement.Application.ReadModel.Queries.Articles.LatestArticles;

public class LatestArticlesQuery : IRequest<List<ArticleQueryModel>> { }
