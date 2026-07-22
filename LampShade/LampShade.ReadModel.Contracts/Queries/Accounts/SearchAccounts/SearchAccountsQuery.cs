#nullable enable

using AccountManagement.Application.Contracts.AC.Account;
using MediatR;

namespace LampShade.ReadModel.Contracts.Queries.Accounts.SearchAccounts;

public class SearchAccountsQuery : IRequest<List<AccountViewModel>>
{
    public string? FullName { get; set; }
    public string? UserName { get; set; }
    public string? Mobile { get; set; }
    public long? RoleId { get; set; }
}
