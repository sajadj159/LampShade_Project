using _01_LampShadeQuery.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleCategoryModel : PageModel
    {
        public ArticleCategoryQueryModel ArticleCategory;
        private readonly IArticleCategoryQuery _categoryQuery;

        public ArticleCategoryModel(IArticleCategoryQuery categoryQuery)
        {
            _categoryQuery = categoryQuery;
        }

        public void OnGet(string id)
        {
            ArticleCategory = _categoryQuery.GetArticleCategories(id);
        }
    }
}
