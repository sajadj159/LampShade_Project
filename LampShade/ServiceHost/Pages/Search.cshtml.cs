using System.Collections.Generic;
using _01_LampShadeQuery.Contract.Product;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class SearchModel : PageModel
    {
        public string Value;
        public List<ProductQueryModel> Products;
        private readonly IProductQuery _product;

        public SearchModel(IProductQuery product)
        {
            _product = product;
        }

        public void OnGet(string value)
        {
            Value = value;
            Products = _product.Search(value);
        }
    }
}
