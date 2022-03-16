using _0_Framework.Application;
using AccountManagement.Application.Contracts.AC.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    [Authorize]
    public class AddressModel : PageModel
    {
        public MakeAddress Command;
        public string Address { get; set; }
        public string PostalCode { get; set; }
        [TempData] public string Message { get; set; }
        private readonly IAuthHelper _authHelper;
        private readonly IAccountApplication _accountApplication;
        public AddressModel(IAuthHelper authHelper, IAccountApplication accountApplication)
        {
            _authHelper = authHelper;
            _accountApplication = accountApplication;
        }

        public void OnGet()
        {
            if (!_authHelper.IsAuthenticated())
            {
                return;
            }

            var currentAccountId = _authHelper.CurrentAccountId();
            Command = _accountApplication.GetAddressBy(currentAccountId);
            if (string.IsNullOrWhiteSpace(Command.Address) && string.IsNullOrWhiteSpace(Command.PostalCode))
            {
                Address = string.Empty;
                PostalCode = string.Empty;
                return;
            }
            Address = Command.Address;
            PostalCode = Command.PostalCode;
        }
        public RedirectToPageResult OnPostMakeAddress(MakeAddress command)
        {
            command.AccountId = _authHelper.CurrentAccountId();
            var operationResult = _accountApplication.MakeAddress(command);
            if (operationResult.IsSucceeded)
            {
                return RedirectToPage("/Checkout");
            }

            Message = operationResult.Message;
            return RedirectToPage("Address");
        }
    }
}
