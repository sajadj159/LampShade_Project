using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contract.A.Comment;

namespace ServiceHost.Areas.Administration.Pages.Shop.Comments
{
    public class IndexModel : PageModel
    {
        [TempData] public string Message { get; set; }
        public List<CommentViewModel> Comments;
        public CommentSearchModel SearchModel;

        private readonly ICommentApplication _comment;

        public IndexModel(ICommentApplication comment)
        {
            _comment = comment;
        }


        public void OnGet(CommentSearchModel searchModel)
        {
            Comments = _comment.Search(searchModel);
        }

        public RedirectToPageResult OnGetConfirm(long id)
        {
            var result= _comment.Confirm(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public RedirectToPageResult OnGetCancel(long id)
        {
           var result= _comment.Cancel(id);
           if (result.IsSucceeded)
               return RedirectToPage("./Index");

           Message = result.Message;
           return RedirectToPage("./Index");
        }
    }
}
