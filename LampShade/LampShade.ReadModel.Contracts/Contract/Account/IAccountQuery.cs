using System.Collections.Generic;

namespace LampShade.ReadModel.Contracts.Account
{
    public interface IAccountQuery
    {
        List<AccountQueryModel> GetAccounts();
    }
}