using System.Collections.Generic;
using LampShade.ReadModel.Contracts.ArticleCategories.Dto;

namespace LampShade.ReadModel.Contracts.ArticleCategory
{
    public interface IArticleCategoryQuery
    {
        ArticleCategoryQueryModel GetArticleCategory(string slug);
        List<ArticleCategoryQueryModel> GetArticleCategories();
        ArticleCategoryDetailsDto GetArticleCategoryForManagement(long id);
        List<ArticleCategoryViewModel> GetArticleCategoriesForManagement();
        List<ArticleCategoryViewModel> SearchArticleCategories(string name);
    }
}


