using System.Collections.Generic;
using _0_Framework.Domain;
using AccountManagement.Application.Contracts.AC.Account;

namespace AccountManagement.Domain.AccountAgg
{
    public interface IAccountRepository : IRepository<long,Account>
    {
        Account GetBy(string userName);
        List<AccountViewModel> Search(AccountSearchModel searchModel);
        List<AccountViewModel> GetAccounts();
        EditAccount GetDetails(long id);
    }
}