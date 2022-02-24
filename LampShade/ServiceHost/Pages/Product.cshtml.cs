using _01_LampShadeQuery.Contract.Product;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        public ProductQueryModel Product;
        private readonly IProductQuery _product;

        public ProductModel(IProductQuery product)
        {
            _product = product;
        }

        public void OnGet(string id)
        {
            Product = _product.GetDetails(id);
        }
    }
}
