using System.Collections.Generic;
using System.Linq;
using _0_Framework.Repository;
using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;

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
			return _context.Roles.AsNoTracking().ToList();
		}

		public Role GetDetails(long id)
		{
			return _context.Roles.AsNoTracking().Single(a => a.Id == id);
		}

	}
}