using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class RoleRepository(AccountContext context) : RepositoryBase<long, Role>(context), IRoleRepository
    {
    }
}
