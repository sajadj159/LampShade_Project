using _01_LampShadeQuery.Contract.Product;
using CommentManagement.Application.Contract.A.Comment;
using CommentManagement.Infrastructure.EFCore;
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
            Product = _product.GetProductDetails(id);
        }

        public RedirectToPageResult OnPost(AddComment command,string productSlug)
        {
            command.Type = CommentType.Product;
            Comment.Add(command);
            return RedirectToPage("./Product", new { Id = productSlug });
        }
    }
}
