using System.Collections.Generic;
using System.Linq;
using _0_Framework.Repository;
using AccountManagement.Application.Contracts.AC.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
    public class EditModel : PageModel
    {
        public EditRole Command;
        public List<SelectListItem> Permissions =new List<SelectListItem>();
        private readonly IRoleApplication _roleApplication;
        private readonly IEnumerable<IPermissionExposer> _permissionsExposers;

        public EditModel(IRoleApplication roleApplication, IEnumerable<IPermissionExposer> permissionsExposers)
        {
            _roleApplication = roleApplication;
            _permissionsExposers = permissionsExposers;
        }

        public void OnGet(long id)
        {
            Command = _roleApplication.GetDetails(id);
            var permissions = new List<PermissionDto>();
            foreach (var exposer in _permissionsExposers)
            {
                var exposedPermissions = exposer.Expose();
                foreach (var (key, value) in exposedPermissions)
                {
                    permissions.AddRange(value);
                    var group = new SelectListGroup
                    {
                        Name = key
                    };
                    foreach (var permission in value)
                    {
                        var item = new SelectListItem(permission.Name, permission.Code.ToString())
                        {
                            Group = group
                        };
                        if (Command.MappedPermissions.Any(x => x.Code == permission.Code))
                            item.Selected = true;

                        Permissions.Add(item);

                    }
                }
            }
        }

        public RedirectToPageResult OnPost(EditRole command)
        {
            var result = _roleApplication.Edit(command);
            return RedirectToPage("Index");
        }
    }
}
