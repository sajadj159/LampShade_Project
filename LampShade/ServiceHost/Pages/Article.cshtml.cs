using System.Collections.Generic;
using _01_LampShadeQuery.Contract.Article;
using _01_LampShadeQuery.Contract.ArticleCategory;
using CommentManagement.Application.Contract.A.Comment;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.ProjectModel;

namespace ServiceHost.Pages
{
    public class ArticleModel : PageModel
    {
        public ArticleQueryModel Article;
        public List<ArticleQueryModel> LatestArticles;
        public List<ArticleCategoryQueryModel> ArticleCategories;
        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _categoryQuery;
        private readonly ICommentApplication _commentApplication;

        public ArticleModel(IArticleQuery articleQuery, IArticleCategoryQuery categoryQuery, ICommentApplication commentApplication)
        {
            _articleQuery = articleQuery;
            _categoryQuery = categoryQuery;
            _commentApplication = commentApplication;
        }

        public void OnGet(string id)
        {
            LatestArticles = _articleQuery.LatestArticles();
            Article = _articleQuery.GetArticleDetails(id);
            ArticleCategories = _categoryQuery.GetArticleCategories();
        }

        public RedirectToPageResult OnPost(AddComment command, string articleSlug)
        {
            command.Type = CommentType.Article;
            var result = _commentApplication.Add(command);
            return RedirectToPage("/Article", new {Id = articleSlug});
        }
    }
}
