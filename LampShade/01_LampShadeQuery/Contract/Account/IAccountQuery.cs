using System.Collections.Generic;

namespace _01_LampShadeQuery.Contract.Account
{
    public interface IAccountQuery
    {
        List<AccountQueryModel> GetAccounts();
    }
}