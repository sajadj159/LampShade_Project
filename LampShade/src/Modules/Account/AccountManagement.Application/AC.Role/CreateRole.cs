using System.Collections.Generic;
using AccountManagement.Domain.RoleAgg.Contracts;

namespace AccountManagement.Application.Contracts.AC.Role
{
    public class CreateRole
    {
        public string Name { get; set; }
        public List<CreatePermissionDto> Permissions { get; set; }

    }
}