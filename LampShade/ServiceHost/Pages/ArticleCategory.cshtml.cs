using System.Collections.Generic;
using _01_LampShadeQuery.Contract.Article;
using _01_LampShadeQuery.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleCategoryModel : PageModel
    {
        public ArticleCategoryQueryModel ArticleCategory;
        public List<ArticleCategoryQueryModel> ArticleCategories;
        public List<ArticleQueryModel> LatestArticles;
        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _categoryQuery;

        public ArticleCategoryModel(IArticleCategoryQuery categoryQuery, IArticleQuery articleQuery)
        {
            _categoryQuery = categoryQuery;
            _articleQuery = articleQuery;
        }

        public void OnGet(string id)
        {
            ArticleCategory = _categoryQuery.GetArticleCategory(id);
            ArticleCategories = _categoryQuery.GetArticleCategories();
            LatestArticles = _articleQuery.LatestArticles();
        }
    }
}
