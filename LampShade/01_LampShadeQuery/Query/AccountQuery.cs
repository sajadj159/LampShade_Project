using System.Collections.Generic;
using System.Linq;
using _01_LampShadeQuery.Contract.Account;
using AccountManagement.Infrastructure.EFCore;

namespace _01_LampShadeQuery.Query
{
    public class AccountQuery : IAccountQuery
    {
        private readonly AccountContext _context;

        public AccountQuery(AccountContext context)
        {
            _context = context;
        }

        public List<AccountQueryModel> GetAccounts()
        {
            return _context.Accounts.Select(x => new AccountQueryModel
            {
                Id = x.Id,
                UserName = x.UserName,
            }).ToList();
        }
    }
}