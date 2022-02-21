using _01_LampShadeQuery.Contract.ProductCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductCategoryModel : PageModel
    {
        public ProductCategoryQueryModel ProductCategory;
        private readonly IProductCategoryQuery _productCategory;

        public ProductCategoryModel(IProductCategoryQuery productCategory)
        {
            _productCategory = productCategory;
        }

        public void OnGet(string id)
        {
             ProductCategory = _productCategory.GetProductCategoryWithProducts(id);
        }
    }
}
