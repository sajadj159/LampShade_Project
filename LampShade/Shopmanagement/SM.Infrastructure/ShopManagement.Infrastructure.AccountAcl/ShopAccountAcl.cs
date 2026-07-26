using System.Threading;
using System.Threading.Tasks;
using LampShade.ReadModel.Contracts.Account;
using ShopManagement.Domain.Services;

namespace ShopManagement.Infrastructure.AccountAcl;

public class ShopAccountAcl(IAccountQuery accountQuery) : IShopAccountAcl
{
    public async Task<(string name, string mobile)> GetAccountByAsync(long id, CancellationToken cancellationToken = default)
    {
        var account = await accountQuery.GetAccountAsync(id, cancellationToken);
        return (account?.FullName ?? string.Empty, account?.Mobile ?? string.Empty);
    }
}