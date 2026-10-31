using BlogManagement.Application.ReadModel.ArticleCategories.Dto;
using MediatR;

namespace BlogManagement.Application.ReadModel.Queries.ArticleCategories.GetArticleCategories;

public class GetArticleCategoriesQuery : IRequest<List<ArticleCategoryViewModel>> { }

