using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.A.Product;
using ShopManagement.Application.Contract.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.Products
{
    public class IndexModel : PageModel
    {
        [TempData] public string Message { get; set; }
        public List<ProductViewModel> Products;
        public ProductSearchModel SearchModel;
        public SelectList ProductCategories;
        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;

        public IndexModel(IProductApplication productApplication, IProductCategoryApplication productCategoryApplication)
        {
            _productApplication = productApplication;
            _productCategoryApplication = productCategoryApplication;
        }


        public void OnGet(ProductSearchModel searchModel)
        {
            ProductCategories = new SelectList(_productCategoryApplication.GetProductCategories(),"Id","Name");
            Products = _productApplication.Search(searchModel);
        }

        public PartialViewResult OnGetCreate()
        {
            var command = new CreateProduct
            {
                Products = _productCategoryApplication.GetProductCategories()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateProduct command)
        {
            var result = _productApplication.Create(command);
            return new JsonResult(result);
        }

        public PartialViewResult OnGetEdit(long id)
        {
            var editProduct = _productApplication.GetDetails(id);
            editProduct.Products = _productCategoryApplication.GetProductCategories();
            return Partial("Edit", editProduct);
        }

        public JsonResult OnPostEdit(EditProduct command)
        {
            var result = _productApplication.Edit(command);
            return new JsonResult(result);
        }

        public RedirectToPageResult OnGetIsInStock(long id)
        {
            var result= _productApplication.InStock(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public RedirectToPageResult OnGetNotInStock(long id)
        {
           var result= _productApplication.NotInStock(id);
           if (result.IsSucceeded)
               return RedirectToPage("./Index");

           Message = result.Message;
           return RedirectToPage("./Index");
        }
    }
}
