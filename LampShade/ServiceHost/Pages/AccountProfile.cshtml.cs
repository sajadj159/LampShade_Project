using _0_Framework.Application;
using AccountManagement.Application.Contracts.AC.Account;
using AccountManagement.Application.Contracts.AC.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class AccountProfileModel : PageModel
    {
        public EditAccount EditAccount { get; set; } 
        public OperationResult Result { get; set; }
        private readonly IAuthHelper _authHelper;
        private readonly IAccountApplication _accountApplication;

        public AccountProfileModel(IAuthHelper authHelper, IAccountApplication accountApplication, IRoleApplication roleApplication)
        {
            _authHelper = authHelper;
            _accountApplication = accountApplication;
            Result = new OperationResult();
        }

        public void OnGet()
        {
            if (!_authHelper.IsAuthenticated())
            {
                return;
            }
            var currentAccountId = _authHelper.CurrentAccountId();
            EditAccount = _accountApplication.GetDetails(currentAccountId);
        }

        public RedirectToPageResult OnPostEditProfile(EditAccount command)
        {
            Result = _accountApplication.Edit(command);
            if (Result.IsSucceeded)
            {
                return RedirectToPage("AccountProfile");
            }

            return RedirectToPage("AccountProfile");
        }
    }
}
