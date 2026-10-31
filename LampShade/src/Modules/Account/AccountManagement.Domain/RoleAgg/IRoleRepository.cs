using System.Collections.Generic;
using _0_Framework.Domain;

namespace AccountManagement.Domain.RoleAgg
{
    public interface IRoleRepository : IRepository<long, Role>
    {
        List<Role> GetRolls();
        Role GetDetails(long id);
    }
}