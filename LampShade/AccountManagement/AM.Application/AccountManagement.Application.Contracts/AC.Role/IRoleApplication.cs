using System.Collections.Generic;
using _0_Framework.Application;
using AccountManagement.Domain.RoleAgg.Contracts;

namespace AccountManagement.Application.Contracts.AC.Role
{
    public interface IRoleApplication
    {
        OperationResult Create(CreateRole command);
        OperationResult Edit(EditRole command);
        List<RoleDto> GetRolls();
        RoleDto GetDetails(long id);
    }
}