using _01_LampShadeQuery.Contract.Product;
using CommentManagement.Application.Contract.A.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        public ProductQueryModel Product;
        private readonly IProductQuery _product;
        protected readonly ICommentApplication Comment;
        public ProductModel(IProductQuery product, ICommentApplication comment)
        {
            _product = product;
            Comment = comment;
        }

        public void OnGet(string id)
        {
            Product = _product.GetDetails(id);
        }

        public RedirectToPageResult OnPost(AddComment command,string productSlug)
        {
            Comment.Add(command);
            return RedirectToPage("./Product", new { Id = productSlug });
        }
    }
}
