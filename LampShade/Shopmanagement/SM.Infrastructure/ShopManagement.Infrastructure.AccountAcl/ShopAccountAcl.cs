using LampShade.ReadModel.Contracts.Account;
using ShopManagement.Domain.Services;

namespace ShopManagement.Infrastructure.AccountAcl;

public class ShopAccountAcl(IAccountQuery accountQuery) : IShopAccountAcl
{
    public (string name, string mobile) GetAccountBy(long id)
    {
        var account = accountQuery.GetAccount(id);
        return (account.FullName, account.Mobile);
    }
}
