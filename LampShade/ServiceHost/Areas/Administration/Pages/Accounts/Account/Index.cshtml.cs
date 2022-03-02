using System.Collections.Generic;
using AccountManagement.Application.Contracts.AC.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Account
{
    public class IndexModel : PageModel
    {
        [TempData] public string Message { get; set; }
        public List<AccountViewModel> Accounts;
        public AccountSearchModel SearchModel;
        public SelectList Roles;
        private readonly IAccountApplication _accountApplication;

        public IndexModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }


        public void OnGet(AccountSearchModel searchModel)
        {
            Accounts = _accountApplication.Search(searchModel);
        }

        public PartialViewResult OnGetCreate()
        {
            var command = new CreateAccount
            {
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateAccount command)
        {
            var result = _accountApplication.Create(command);
            return new JsonResult(result);
        }

        public PartialViewResult OnGetEdit(long id)
        {
            var editProduct = _accountApplication.GetDetails(id);
            return Partial("Edit", editProduct);
        }

        public JsonResult OnPostEdit(EditAccount command)
        {
            var result = _accountApplication.Edit(command);
            return new JsonResult(result);
        }
        public PartialViewResult OnGetChangePassword(long id)
        {
            var password = new ChangePassword{Id = id};    
            return Partial("ChangePassword", password);
        }

        public JsonResult OnPostChangePassword(ChangePassword command)
        {
            var result = _accountApplication.ChangePassword(command);
            return new JsonResult(result);
        }
    }
}
