using BlogManagement.Application.Contract.AC.Article;
using BlogManagement.Application.Contract.AC.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles
{
    public class EditModel : PageModel
    {
        public EditArticle Command;
        public SelectList ArticleCategories;
        private readonly IArticleCategoryApplication _categoryApplication;
        private readonly IArticleApplication _articleApplication;

        public EditModel(IArticleCategoryApplication categoryApplication, IArticleApplication articleApplication)
        {
            _categoryApplication = categoryApplication;
            _articleApplication = articleApplication;
        }

        public void OnGet(long id)
        {
            Command = _articleApplication.GetDetails(id);
            ArticleCategories = new SelectList(_categoryApplication.GetArticleCategories(), "Id", "Name");
        }

        public RedirectToPageResult OnPost(EditArticle command)
        {
            var result = _articleApplication.Edit(command);
            return RedirectToPage("./Index");
        }
    }
}
