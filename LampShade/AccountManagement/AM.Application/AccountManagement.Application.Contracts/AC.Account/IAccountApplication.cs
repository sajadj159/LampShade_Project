using System.Collections.Generic;
using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.AC.Account
{
    public interface IAccountApplication
    {
        OperationResult Register(RegisterAccount command);
        OperationResult Edit(EditAccount command);
        OperationResult ChangePassword(ChangePassword command);
        OperationResult Login(Login command);
        List<AccountViewModel> Search(AccountSearchModel searchModel);
        List<AccountViewModel> GetAccounts();
        EditAccount GetDetails(long id);

        void Logout();
    }
}