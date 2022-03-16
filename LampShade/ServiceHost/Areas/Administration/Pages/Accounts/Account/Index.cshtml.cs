using System.Collections.Generic;
using AccountManagement.Application.Contracts.AC.Account;
using AccountManagement.Application.Contracts.AC.Role;
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

        private readonly IRoleApplication _roleApplication;
        private readonly IAccountApplication _accountApplication;

        public IndexModel(IAccountApplication accountApplication, IRoleApplication roleApplication)
        {
            _accountApplication = accountApplication;
            _roleApplication = roleApplication;
        }


        public void OnGet(AccountSearchModel searchModel)
        {
            Roles = new SelectList(_roleApplication.GetRolls(), "Id", "Name");
            Accounts = _accountApplication.Search(searchModel);
        }

        public PartialViewResult OnGetCreate()
        {
            var command = new RegisterAccount
            {
               Roles = _roleApplication.GetRolls()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(RegisterAccount command)
        {
            var result = _accountApplication.Register(command);
            return new JsonResult(result);
        }

        public PartialViewResult OnGetEdit(long id)
        {
            var account = _accountApplication.GetDetails(id);
            account.Roles = _roleApplication.GetRolls();
            return Partial("Edit", account);
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
        public PartialViewResult OnGetMakeAddress(long id)
        {
            var result = _accountApplication.GetAddressBy(id);
            return Partial("MakeAddress", result);
        }

        public JsonResult OnPostMakeAddress(MakeAddress command)
        {
            var result = _accountApplication.MakeAddress(command);
            return new JsonResult(result);
        }
    }
}
