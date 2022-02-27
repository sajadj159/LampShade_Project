using System.Collections.Generic;
using BlogManagement.Application.Contract.AC.Article;
using BlogManagement.Application.Contract.AC.ArticleCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles
{
    public class IndexModel : PageModel
    {
        public List<ArticleViewModel> Articles;
        public ArticleSearchModel SearchModel;
        public SelectList ArticleCategories;
        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _articleCategoryApplication;
        public IndexModel(IArticleApplication articleApplication, IArticleCategoryApplication articleCategoryApplication)
        {
            _articleApplication = articleApplication;
            _articleCategoryApplication = articleCategoryApplication;
        }


        public void OnGet(ArticleSearchModel searchModel)
        {
            ArticleCategories = new SelectList(_articleCategoryApplication.GetArticleCategories(), "Id", "Name");
            Articles = _articleApplication.Search(searchModel);
        }
    }
}
