#nullable enable

using AccountManagement.Application.ReadModel.Accounts.Dto;
using MediatR;

namespace AccountManagement.Application.ReadModel.Queries.Accounts.SearchAccounts;

public class SearchAccountsQuery : IRequest<List<AccountViewModel>>
{
    public string? FullName { get; set; }
    public string? UserName { get; set; }
    public string? Mobile { get; set; }
    public long? RoleId { get; set; }
}

