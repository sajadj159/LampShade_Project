using _0_Framework.Application;
using AccountManagement.Infrastructure.EFCore;
using LampShade.ReadModel.Contracts.Account;
using LampShade.ReadModel.Contracts.Accounts.Dto;
using LampShade.ReadModel.Contracts.Roles.Dto;
using Microsoft.EntityFrameworkCore;

namespace LampShade.ReadModel.Application.Query;

public class AccountQuery(AccountContext context) : IAccountQuery
{
    public List<AccountQueryModel> GetAccounts() => context.Accounts.AsNoTracking().Select(x => new AccountQueryModel { Id = x.Id, UserName = x.UserName }).ToList();

    public AccountViewModel GetAccount(long id) => ProjectAccounts(context.Accounts.Where(x => x.Id == id)).FirstOrDefault();

    public List<AccountViewModel> GetAccountsForManagement() => ProjectAccounts(context.Accounts);

    public List<AccountViewModel> SearchAccounts(string fullName, string userName, string mobile, long roleId)
    {
        var query = context.Accounts.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(fullName)) query = query.Where(x => x.FullName.Contains(fullName));
        if (!string.IsNullOrWhiteSpace(userName)) query = query.Where(x => x.UserName.Contains(userName));
        if (!string.IsNullOrWhiteSpace(mobile)) query = query.Where(x => x.Mobile.Contains(mobile));
        if (roleId != 0) query = query.Where(x => x.RoleId == roleId);
        return ProjectAccounts(query);
    }

    public RoleDto GetRole(long id) => context.Roles.AsNoTracking().Where(x => x.Id == id).Select(x => new RoleDto { Id = x.Id, Name = x.Name, CreationDate = x.CreationDate.ToFarsi() }).FirstOrDefault();

    public List<RoleDto> GetRoles() => context.Roles.AsNoTracking().Select(x => new RoleDto { Id = x.Id, Name = x.Name, CreationDate = x.CreationDate.ToFarsi() }).ToList();

    private static List<AccountViewModel> ProjectAccounts(IQueryable<AccountManagement.Domain.AccountAgg.Account> query) => query.AsNoTracking().Select(x => new AccountViewModel { Id = x.Id, FullName = x.FullName, UserName = x.UserName, Mobile = x.Mobile, Address = x.Address, PostalCode = x.PostalCode, Role = x.Role.Name, RoleId = x.RoleId, ProfilePhoto = x.ProfilePhoto, CreationDate = x.CreationDate.ToFarsi() }).ToList();
}
