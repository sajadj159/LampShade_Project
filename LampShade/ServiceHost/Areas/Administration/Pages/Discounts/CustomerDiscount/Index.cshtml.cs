using System.Collections.Generic;
using DiscountManagement.Application.Contract.AC.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.A.Product;

namespace ServiceHost.Areas.Administration.Pages.Discounts.CustomerDiscount
{
    public class IndexModel : PageModel
    {
        [TempData] public string Message { get; set; }
        public List<CustomerDiscountViewmodel> CustomerDiscounts;
        public CustomerDiscountSearchModel SearchModel;
        public SelectList Products;
        private readonly IProductApplication _productApplication;
        private readonly ICustomerDiscountApplication _customerDiscountApplication;

        public IndexModel(IProductApplication productApplication, ICustomerDiscountApplication customerDiscountApplication)
        {
            _productApplication = productApplication;
            _customerDiscountApplication = customerDiscountApplication;
        }


        public void OnGet(CustomerDiscountSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            CustomerDiscounts = _customerDiscountApplication.Search(searchModel);
        }

        public PartialViewResult OnGetCreate()
        {
            var command = new DefineCustomerDiscount
            {
                Products = _productApplication.GetProducts()
            };
            
            return Partial("./Create",command);
        }

        public JsonResult OnPostCreate(DefineCustomerDiscount command)
        {
            var result = _customerDiscountApplication.Define(command);
            return new JsonResult(result);
        }

        public PartialViewResult OnGetEdit(long id)
        {
            var editCustomerDiscount = _customerDiscountApplication.GetDetails(id);
           editCustomerDiscount.Products = _productApplication.GetProducts();
            return Partial("Edit", editCustomerDiscount);
        }

        public JsonResult OnPostEdit(EditCustomerDiscount command)
        {
            var result = _customerDiscountApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
