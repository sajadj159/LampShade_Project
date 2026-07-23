using System.Threading;
using System.Threading.Tasks;
#nullable enable

using LampShade.ReadModel.Contracts.Account;
using LampShade.ReadModel.Contracts.Accounts.Dto;
using LampShade.ReadModel.Contracts.Queries.Accounts.SearchAccounts;
using MediatR;

namespace LampShade.ReadModel.Application.Features.Accounts.Queries.SearchAccounts;

public class SearchAccountsQueryHandler(IAccountQuery query) : IRequestHandler<SearchAccountsQuery, List<AccountViewModel>>
{
    public Task<List<AccountViewModel>> Handle(SearchAccountsQuery request, CancellationToken cancellationToken) =>
        Task.FromResult(query.SearchAccounts(request.FullName ?? string.Empty, request.UserName ?? string.Empty, request.Mobile ?? string.Empty, request.RoleId ?? 0));
}
