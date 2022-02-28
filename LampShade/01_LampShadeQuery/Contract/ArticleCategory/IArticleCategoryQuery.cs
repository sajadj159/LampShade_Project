using System.Collections.Generic;

namespace _01_LampShadeQuery.Contract.ArticleCategory
{
    public interface IArticleCategoryQuery
    {
        ArticleCategoryQueryModel GetArticleCategories(string slug);
        List<ArticleCategoryQueryModel> GetArticleCategories();
    }
}