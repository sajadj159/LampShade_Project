using System.Collections.Generic;
using System.Linq;
using _0_Framework.Repository;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
	public class RoleRepository : RepositoryBase<long, Role>, IRoleRepository
	{
		private readonly AccountContext _context;
		public RoleRepository(AccountContext context) : base(context)
		{
			_context = context;
		}

		public List<Role> GetRolls()
		{
			return _context.Roles.ToList();
		}

		public Role GetDetails(long id)
		{
			return _context.Roles.Single(a => a.Id == id);
		}

	}
}