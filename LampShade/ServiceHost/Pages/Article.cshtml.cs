using System.Collections.Generic;
using _01_LampShadeQuery.Contract.Article;
using _01_LampShadeQuery.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleModel : PageModel
    {
        public ArticleQueryModel Article;
        public List<ArticleQueryModel> LatestArticles;
        public List<ArticleCategoryQueryModel> ArticleCategories;
        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _categoryQuery;

        public ArticleModel(IArticleQuery articleQuery, IArticleCategoryQuery categoryQuery)
        {
            _articleQuery = articleQuery;
            _categoryQuery = categoryQuery;
        }

        public void OnGet(string id)
        {
            LatestArticles = _articleQuery.LatestArticles();
            Article = _articleQuery.GetArticleDetails(id);
            ArticleCategories = _categoryQuery.GetArticleCategories();
        }
    }
}
