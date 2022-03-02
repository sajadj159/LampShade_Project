using System.Collections.Generic;
using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.AC.Role
{
    public class CreateRole
    {
        public string Name { get; set; }

    }

    public class EditRole : CreateRole
    {
        public long Id { get; set; }
    }

    public interface IRoleApplication
    {
        OperationResult Create(CreateRole command);
        OperationResult Edit(EditRole command);
        List<RoleViewModel> GetRolls();
        EditRole GetDetails(long id);
    }

}