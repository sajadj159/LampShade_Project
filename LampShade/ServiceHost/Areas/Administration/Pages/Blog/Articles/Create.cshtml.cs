using BlogManagement.Application.Contract.AC.Article;
using BlogManagement.Application.Contract.AC.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles
{
    public class CreateModel : PageModel
    {
        public CreateArticle Command;
        public SelectList ArticleCategories;
        private readonly IArticleCategoryApplication _categoryApplication;
        private readonly IArticleApplication _articleApplication;

        public CreateModel(IArticleCategoryApplication categoryApplication, IArticleApplication articleApplication)
        {
            _categoryApplication = categoryApplication;
            _articleApplication = articleApplication;
        }

        public void OnGet()
        {
            ArticleCategories = new SelectList(_categoryApplication.GetArticleCategories(), "Id", "Name");
        }

        public RedirectToPageResult OnPost(CreateArticle command)
        {
            var result = _articleApplication.Create(command);
            return RedirectToPage("./Index");
        }
    }
}
