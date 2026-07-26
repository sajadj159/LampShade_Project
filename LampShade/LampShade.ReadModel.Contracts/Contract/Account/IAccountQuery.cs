using LampShade.ReadModel.Contracts.Accounts.Dto;
using LampShade.ReadModel.Contracts.Roles.Dto;

namespace LampShade.ReadModel.Contracts.Account;

public interface IAccountQuery
{
    Task<List<AccountQueryModel>> GetAccountsAsync(CancellationToken cancellationToken = default);
    Task<AccountViewModel> GetAccountAsync(long id, CancellationToken cancellationToken = default);
    Task<List<AccountViewModel>> GetAccountsForManagementAsync(CancellationToken cancellationToken = default);
    Task<List<AccountViewModel>> SearchAccountsAsync(string fullName, string userName, string mobile, long roleId, CancellationToken cancellationToken = default);
    Task<RoleDto> GetRoleAsync(long id, CancellationToken cancellationToken = default);
    Task<List<RoleDto>> GetRolesAsync(CancellationToken cancellationToken = default);
}