using _01_LampShadeQuery.Contract.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contract.A.Comment;
using ShopManagement.Infrastructure.EFCore.Migrations;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        public ProductQueryModel Product;
        private readonly IProductQuery _product;
        protected readonly ICommentApplication _comment;
        public ProductModel(IProductQuery product, ICommentApplication comment)
        {
            _product = product;
            _comment = comment;
        }

        public void OnGet(string id)
        {
            Product = _product.GetDetails(id);
        }

        public RedirectToPageResult OnPost(AddComment command,string productSlug)
        {
            var result = _comment.Add(command);
            return RedirectToPage("./Product", new { Id = productSlug });
        }
    }
}
