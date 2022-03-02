using System.Collections.Generic;
using _0_Framework.Domain;
using AccountManagement.Application.Contracts.AC.Role;

namespace AccountManagement.Domain.RoleAgg
{
    public interface IRoleRepository : IRepository<long, Role>
    {
        List<RoleViewModel> GetRolls();
        EditRole GetDetails(long id);
    }
}