using System.Collections.Generic;
using AccountManagement.Application.Contracts.AC.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.Order;

namespace ServiceHost.Areas.Administration.Pages.Shop.Orders
{
    public class IndexModel : PageModel
    {
        public List<OrderViewModel> Orders;
        public OrderSearchModel SearchModel;
        public SelectList Accounts;
        private readonly IOrderApplication _orderApplication;
        private readonly IAccountApplication _accountApplication;

        public IndexModel(IOrderApplication orderApplication, IAccountApplication accountApplication)
        {
            _orderApplication = orderApplication;
            _accountApplication = accountApplication;
        }

        public void OnGet(OrderSearchModel searchModel)
        {
            Accounts = new SelectList(_accountApplication.GetAccounts(), "Id", "FullName");
            Orders = _orderApplication.Search(searchModel);
        }

        public RedirectToPageResult OnGetConfirm(long id)
        {
            _orderApplication.PaymentSucceeded(id, 0);
            return RedirectToPage("Index");
        }

        public RedirectToPageResult OnGetCancel(long id)
        {
            _orderApplication.Cancel(id);
            return RedirectToPage("Index");
        }

        public PartialViewResult OnGetItems(long id)
        {
            var items = _orderApplication.GetItemsBy(id);
            return Partial("Items", items);
        }

    }
}
