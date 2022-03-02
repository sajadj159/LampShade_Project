using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Repository;
using AccountManagement.Application.Contracts.AC.Role;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class RoleRepository:RepositoryBase<long,Role>,IRoleRepository
    {
        private readonly AccountContext _context;
        public RoleRepository(AccountContext context) : base(context)
        {
            _context = context;
        }

        public List<RoleViewModel> GetRolls()
        {
            return _context.Roles.Select(x => new RoleViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CreationDate = x.CreationDate.ToFarsi()
            }).OrderByDescending(x=>x.Id).ToList();
        }

        public EditRole GetDetails(long id)
        {
            return _context.Roles.Select(x => new EditRole
            {
                Id = x.Id,
                Name = x.Name
            }).FirstOrDefault(x => x.Id == id);
        }
    }
}