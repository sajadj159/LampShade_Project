using System.Collections.Generic;
using DiscountManagement.Application.Contract.AC.ColleagueDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.A.Product;

namespace ServiceHost.Areas.Administration.Pages.Discounts.ColleagueDiscount
{
    public class IndexModel : PageModel
    {
        [TempData] public string Message { get; set; }
        public List<ColleagueDiscountViewModel> ColleagueDiscounts;
        public ColleagueDiscountSearchModel SearchModel;
        public SelectList Products;
        private readonly IProductApplication _productApplication;
        private readonly IColleagueDiscountApplication _colleagueDiscountApplication;
        public IndexModel(IProductApplication productApplication, IColleagueDiscountApplication colleagueDiscountApplication)
        {
            _productApplication = productApplication;
            _colleagueDiscountApplication = colleagueDiscountApplication;
        }


        public void OnGet(ColleagueDiscountSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            ColleagueDiscounts = _colleagueDiscountApplication.Search(searchModel);
        }

        public PartialViewResult OnGetCreate()
        {
            var command = new DefineColleagueDiscount
            {
                Products = _productApplication.GetProducts()
            };
            
            return Partial("./Create",command);
        }

        public JsonResult OnPostCreate(DefineColleagueDiscount command)
        {
            var result = _colleagueDiscountApplication.Define(command);
            return new JsonResult(result);
        }

        public PartialViewResult OnGetEdit(long id)
        {
            var editColleagueDiscount = _colleagueDiscountApplication.GetDetails(id);
           editColleagueDiscount.Products = _productApplication.GetProducts();
            return Partial("Edit", editColleagueDiscount);
        }

        public JsonResult OnPostEdit(EditColleagueDiscount command)
        {
            var result = _colleagueDiscountApplication.Edit(command);
            return new JsonResult(result);
        }

        public RedirectToPageResult OnGetRemove(long id)
        {
            var operationResult = _colleagueDiscountApplication.Remove(id);
            if (operationResult.IsSucceeded)
            {
                return RedirectToPage("./Index");
            }

            Message = operationResult.Message;
            return RedirectToPage("./Index");
        }

        public RedirectToPageResult OnGetRestore(long id)
        {
            var operationResult = _colleagueDiscountApplication.Restore(id);
            if (operationResult.IsSucceeded)
            {
                return RedirectToPage("./Index");
            }

            Message = operationResult.Message;
            return RedirectToPage("./Index");
        }
    }
}
