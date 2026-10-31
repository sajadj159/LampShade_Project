using AccountManagement.Contracts;
using AccountManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure;

internal sealed class AccountModuleApi(AccountContext context) : IAccountModuleApi
{
    public async Task<IReadOnlyDictionary<long, AccountSummary>> GetAccountsAsync(IEnumerable<long> accountIds, CancellationToken cancellationToken = default)
    {
        var ids = accountIds.Distinct().ToList();
        return await context.Accounts.AsNoTracking()
            .Where(x => ids.Contains(x.Id))
            .Select(x => new AccountSummary(x.Id, x.FullName, x.Mobile))
            .ToDictionaryAsync(x => x.Id, cancellationToken);
    }
}
