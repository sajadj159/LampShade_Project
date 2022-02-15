using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.A.Product;
using ShopManagement.Application.Contract.A.ProductPicture;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductPictures
{
    public class IndexModel : PageModel
    {
        [TempData] public string Message { get; set; }
        public List<ProductPictureViewModel> ProductPictures;
        public ProductPictureSearchModel SearchModel;
        public SelectList Products;

        private readonly IProductApplication _productApplication;
        private readonly IProductPictureApplication _productPictureApplication;
        public IndexModel(IProductApplication productApplication, IProductPictureApplication productPictureApplication)
        {
            _productApplication = productApplication;
            _productPictureApplication = productPictureApplication;
        }


        public void OnGet(ProductPictureSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(),"Id","Name");
            ProductPictures = _productPictureApplication.Search(searchModel);
        }

        public PartialViewResult OnGetCreate()
        {
            var command = new CreateProductPicture
            {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateProductPicture command)
        {
            var result = _productPictureApplication.Create(command);
            return new JsonResult(result);
        }

        public PartialViewResult OnGetEdit(long id)
        {
            var editProduct = _productPictureApplication.GetDetails(id);
            editProduct.Products = _productApplication.GetProducts();
            return Partial("Edit", editProduct);
        }

        public JsonResult OnPostEdit(EditProductPicture command)
        {
            var result = _productPictureApplication.Edit(command);
            return new JsonResult(result);
        }

        public RedirectToPageResult OnGetRestore(long id)
        {
            var result= _productPictureApplication.Restore(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public RedirectToPageResult OnGetRemove(long id)
        {
           var result= _productPictureApplication.Remove(id);
           if (result.IsSucceeded)
               return RedirectToPage("./Index");

           Message = result.Message;
           return RedirectToPage("./Index");
        }
    }
}
