using System.Collections.Generic;
using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.AC.Role
{
    public interface IRoleApplication
    {
        OperationResult Create(CreateRole command);
        OperationResult Edit(EditRole command);
        List<RoleViewModel> GetRolls();
        EditRole GetDetails(long id);
    }
}