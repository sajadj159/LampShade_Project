using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Application.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
    public class IndexModel : PageModel
    {
        public List<ProductCategoryViewModel> ProductCategories;
        public ProductCategorySearchModel SearchModel;
        private readonly IProductCategoryApplication _categoryApplication;

        public IndexModel(IProductCategoryApplication categoryApplication)
        {
            _categoryApplication = categoryApplication;
        }


        public void OnGet(ProductCategorySearchModel searchModel)
        {
            ProductCategories = _categoryApplication.Search(searchModel);
        }

        public PartialViewResult OnGetCreate()
        {
            return Partial("./Create", new CreateProductCategory());
        }

        public JsonResult OnPostCreate(CreateProductCategory command)
        {
            var result = _categoryApplication.Create(command);
            return new JsonResult(result);
        }
    }
}
