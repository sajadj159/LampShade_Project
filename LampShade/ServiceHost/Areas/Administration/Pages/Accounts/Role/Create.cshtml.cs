using AccountManagement.Application.Contracts.AC.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
    public class CreateModel : PageModel
    {
        public CreateRole Command;
        private readonly IRoleApplication _roleApplication;

        public CreateModel(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        public void OnGet()
        {
            var command = new CreateRole();
        }

        public RedirectToPageResult OnPost(CreateRole command)
        {
            var result = _roleApplication.Create(command);
           return RedirectToPage("Index");
        }
    }
}
