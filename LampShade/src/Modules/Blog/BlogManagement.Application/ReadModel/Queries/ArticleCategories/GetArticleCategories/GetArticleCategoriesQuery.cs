using LampShade.ReadModel.Contracts.ArticleCategories.Dto;
using MediatR;

namespace LampShade.ReadModel.Contracts.Queries.ArticleCategories.GetArticleCategories;

public class GetArticleCategoriesQuery : IRequest<List<ArticleCategoryViewModel>> { }

