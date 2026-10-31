using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class AccountRepository(AccountContext context) : RepositoryBase<long, Account>(context), IAccountRepository
    {
        public Task<Account> GetByAsync(string userName, CancellationToken cancellationToken = default) =>
            context.Accounts.FirstOrDefaultAsync(x => x.UserName == userName || x.Mobile == userName, cancellationToken);
    }
}
