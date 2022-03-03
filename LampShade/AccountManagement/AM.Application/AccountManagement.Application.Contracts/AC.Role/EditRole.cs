using System.Collections.Generic;
using _0_Framework.Repository;

namespace AccountManagement.Application.Contracts.AC.Role
{
    public class EditRole : CreateRole
    {
        public long Id { get; set; }
        public List<PermissionDto> MappedPermissions { get; set; }

    }
}