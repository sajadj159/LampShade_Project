using System.Collections.Generic;
using AccountManagement.Domain.RoleAgg.Contracts;

namespace AccountManagement.Application.Contracts.AC.Role
{
    public class EditRole : CreateRole
    {
        public long Id { get; set; }
        public List<CreatePermissionDto> MappedPermissions { get; set; }

    }
}