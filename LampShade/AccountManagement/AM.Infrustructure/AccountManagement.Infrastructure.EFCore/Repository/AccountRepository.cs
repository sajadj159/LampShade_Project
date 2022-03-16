using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Repository;
using AccountManagement.Application.Contracts.AC.Account;
using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class AccountRepository : RepositoryBase<long, Account>, IAccountRepository
    {
        private readonly AccountContext _context;
        public AccountRepository(AccountContext context) : base(context)
        {
            _context = context;
        }

        public Account GetBy(string userName)
        {
            return _context.Accounts.FirstOrDefault(x => x.UserName == userName);
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            var queryable = _context.Accounts
                .Include(x=>x.Role)
                .Select(x => new AccountViewModel
            {
                Id = x.Id,
                UserName = x.UserName,
                FullName = x.FullName,
                Mobile = x.Mobile,
                ProfilePhoto = x.ProfilePhoto,
                RoleId = x.RoleId,
                Role = x.Role.Name,
                Address = x.Address,
                PostalCode = x.PostalCode,
                CreationDate = x.CreationDate.ToFarsi()

            });
            if (!string.IsNullOrWhiteSpace(searchModel.UserName))
            {
                queryable = queryable.Where(x => x.UserName.Contains(searchModel.UserName));
            }

            if (!string.IsNullOrWhiteSpace(searchModel.FullName))
            {
                queryable = queryable.Where(x => x.FullName.Contains(searchModel.FullName));
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Mobile))
            {
                queryable = queryable.Where(x => x.Mobile.Contains(searchModel.Mobile));
            }

            if (searchModel.RoleId > 0)
            {
                queryable = queryable.Where(x => x.RoleId == searchModel.RoleId);
            }

            return queryable.OrderByDescending(x => x.Id).ToList();
        }

        public List<AccountViewModel> GetAccounts()
        {
            return _context.Accounts.Select(x => new AccountViewModel
            {
                Id = x.Id,
                FullName = x.FullName
            }).ToList();  
        }

        public EditAccount GetDetails(long id)
        {
            return _context.Accounts
                .Select(x => new EditAccount
                {
                    UserName = x.UserName,
                    FullName = x.FullName,
                    Mobile = x.Mobile,
                    RoleId = x.RoleId,
                    Id = x.Id,
                    Address = x.Address,
                    PostalCode = x.PostalCode
                }).FirstOrDefault(x => x.Id == id);
        }

        public MakeAddress GetAddressBy(long id)
        {
            return _context.Accounts.Select(x => new MakeAddress
            {
                AccountId = x.Id,
                Address = x.Address,
                PostalCode = x.PostalCode
            }).FirstOrDefault(x => x.AccountId == id);
        }
    }
}