using System.Collections.Generic;
using System.Linq;
using _0_Framework.Domain;
using AccountManagement.Domain.RoleAgg.Contracts;

namespace AccountManagement.Domain.RoleAgg
{
    public class Role : EntityBase
    {
        public string Name { get; private set; }
        public List<Permission> Permissions { get; private set; }

        protected Role()
        {
        }
        public Role(string name, List<CreatePermissionDto> permissions)
        {
            Name = name;
            Permissions = permissions.Select(a=>new Permission(a.Name,a.Code,Id)).ToList();
        }

        public void Edit(string name, List<CreatePermissionDto> permissions)
        {
            Permissions.Clear();
            Name = name;
            Permissions = permissions.Select(a=>new Permission(a.Name,a.Code,Id)).ToList();
        }
    }
}