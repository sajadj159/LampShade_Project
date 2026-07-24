using _0_Framework.Application;
using AccountManagement.Infrastructure.EFCore;
using LampShade.ReadModel.Contracts.Account;
using LampShade.ReadModel.Contracts.Accounts.Dto;
using LampShade.ReadModel.Contracts.Roles.Dto;
using Microsoft.EntityFrameworkCore;

namespace LampShade.ReadModel.Application.Query;

public class AccountQuery(AccountContext context) : IAccountQuery
{
    public Task<List<AccountQueryModel>> GetAccountsAsync(CancellationToken cancellationToken = default) =>
        context.Accounts.AsNoTracking()
            .Select(x => new AccountQueryModel { Id = x.Id, UserName = x.UserName })
            .ToListAsync(cancellationToken);

    public Task<AccountViewModel> GetAccountAsync(long id, CancellationToken cancellationToken = default) =>
        ProjectAccounts(context.Accounts.AsNoTracking().Where(x => x.Id == id)).FirstOrDefaultAsync(cancellationToken);

    public Task<List<AccountViewModel>> GetAccountsForManagementAsync(CancellationToken cancellationToken = default) =>
        ProjectAccounts(context.Accounts.AsNoTracking()).ToListAsync(cancellationToken);

    public Task<List<AccountViewModel>> SearchAccountsAsync(string fullName, string userName, string mobile, long roleId, CancellationToken cancellationToken = default)
    {
        var query = context.Accounts.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(fullName)) query = query.Where(x => x.FullName.Contains(fullName));
        if (!string.IsNullOrWhiteSpace(userName)) query = query.Where(x => x.UserName.Contains(userName));
        if (!string.IsNullOrWhiteSpace(mobile)) query = query.Where(x => x.Mobile.Contains(mobile));
        if (roleId != 0) query = query.Where(x => x.RoleId == roleId);
        return ProjectAccounts(query).ToListAsync(cancellationToken);
    }

    public Task<RoleDto> GetRoleAsync(long id, CancellationToken cancellationToken = default) =>
        context.Roles.AsNoTracking().Where(x => x.Id == id)
            .Select(x => new RoleDto { Id = x.Id, Name = x.Name, CreationDate = x.CreationDate.ToFarsi() })
            .FirstOrDefaultAsync(cancellationToken);

    public Task<List<RoleDto>> GetRolesAsync(CancellationToken cancellationToken = default) =>
        context.Roles.AsNoTracking()
            .Select(x => new RoleDto { Id = x.Id, Name = x.Name, CreationDate = x.CreationDate.ToFarsi() })
            .ToListAsync(cancellationToken);

    private static IQueryable<AccountViewModel> ProjectAccounts(IQueryable<AccountManagement.Domain.AccountAgg.Account> query) =>
        query.AsNoTracking().Select(x => new AccountViewModel
        {
            Id = x.Id, FullName = x.FullName, UserName = x.UserName, Mobile = x.Mobile,
            Address = x.Address, PostalCode = x.PostalCode, Role = x.Role.Name, RoleId = x.RoleId,
            ProfilePhoto = x.ProfilePhoto, CreationDate = x.CreationDate.ToFarsi()
        });
}