using MediatR;
using LampShade.ReadModel.Contracts.ArticleCategory;

namespace LampShade.ReadModel.Contracts.Queries.ArticleCategories.GetArticleCategoriesForQuery;

public class GetArticleCategoriesForQuery : IRequest<List<ArticleCategoryQueryModel>> { }
