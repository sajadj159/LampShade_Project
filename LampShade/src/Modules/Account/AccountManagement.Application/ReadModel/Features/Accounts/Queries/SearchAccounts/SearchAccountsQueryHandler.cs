using System.Threading;
using System.Threading.Tasks;
#nullable enable

using AccountManagement.Application.ReadModel.Account;
using AccountManagement.Application.ReadModel.Accounts.Dto;
using AccountManagement.Application.ReadModel.Queries.Accounts.SearchAccounts;
using MediatR;

namespace AccountManagement.Application.ReadModel.Features.Accounts.Queries.SearchAccounts;

public class SearchAccountsQueryHandler(IAccountQuery query) : IRequestHandler<SearchAccountsQuery, List<AccountViewModel>>
{
    public Task<List<AccountViewModel>> Handle(SearchAccountsQuery request, CancellationToken cancellationToken) =>
        query.SearchAccountsAsync(request.FullName ?? string.Empty, request.UserName ?? string.Empty, request.Mobile ?? string.Empty, request.RoleId ?? 0, cancellationToken);
}
