using MediatR;
using LampShade.ReadModel.Contracts.Article;

namespace LampShade.ReadModel.Contracts.Queries.Articles.LatestArticles;

public class LatestArticlesQuery : IRequest<List<ArticleQueryModel>> { }
