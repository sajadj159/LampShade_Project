using MediatR;
using BlogManagement.Application.ReadModel.ArticleCategory;

namespace BlogManagement.Application.ReadModel.Queries.ArticleCategories.GetArticleCategoriesForQuery;

public class GetArticleCategoriesForQuery : IRequest<List<ArticleCategoryQueryModel>> { }
