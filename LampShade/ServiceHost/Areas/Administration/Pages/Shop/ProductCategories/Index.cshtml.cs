using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Application.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
    public class IndexModel : PageModel
    {
        public List<ProductCategoryViewModel> ProductCategories { get; set; }
        private readonly IProductCategoryApplication _categoryApplication;

        public IndexModel(ProductCategoryApplication categoryApplication)
        {
            _categoryApplication = categoryApplication;
        }

        public void OnGet(ProductCategorySearchModel searchModel)
        {
            ProductCategories = _categoryApplication.Search(searchModel);
        }
    }
}
