using LampShade.ReadModel.Contracts.Accounts.Dto;
using LampShade.ReadModel.Contracts.Roles.Dto;

namespace LampShade.ReadModel.Contracts.Account;

public interface IAccountQuery
{
    List<AccountQueryModel> GetAccounts();
    AccountViewModel GetAccount(long id);
    List<AccountViewModel> GetAccountsForManagement();
    List<AccountViewModel> SearchAccounts(string fullName, string userName, string mobile, long roleId);
    RoleDto GetRole(long id);
    List<RoleDto> GetRoles();
}
