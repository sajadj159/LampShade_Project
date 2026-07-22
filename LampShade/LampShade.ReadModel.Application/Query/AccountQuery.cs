using System.Collections.Generic;
using System.Linq;
using LampShade.ReadModel.Contracts.Account;
using AccountManagement.Infrastructure.EFCore;

namespace LampShade.ReadModel.Application.Query
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