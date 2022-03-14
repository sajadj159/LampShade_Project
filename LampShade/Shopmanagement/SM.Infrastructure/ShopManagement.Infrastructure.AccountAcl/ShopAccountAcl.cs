using AccountManagement.Application.Contracts.AC.Account;
using ShopManagement.Domain.Services;

namespace ShopManagement.Infrastructure.AccountAcl
{
    public class ShopAccountAcl:IShopAccountAcl
    {
        private readonly IAccountApplication _accountApplication;

        public ShopAccountAcl(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public (string name, string mobile) GetAccountBy(long id)
        {
            var accountViewModel = _accountApplication.GetAccountBy(id);
            return (accountViewModel.FullName, accountViewModel.Mobile);
        }
    }
}