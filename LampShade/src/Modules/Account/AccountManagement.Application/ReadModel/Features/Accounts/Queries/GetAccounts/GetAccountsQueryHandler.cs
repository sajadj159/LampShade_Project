using System.Threading;
using System.Threading.Tasks;
using LampShade.ReadModel.Contracts.Account;
using LampShade.ReadModel.Contracts.Accounts.Dto;
using MediatR;

using LampShade.ReadModel.Contracts.Queries.Accounts.GetAccounts;

namespace LampShade.ReadModel.Application.Features.Accounts.Queries.GetAccounts;



public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, List<AccountViewModel>>
{
    private readonly IAccountQuery _query;

    public GetAccountsQueryHandler(IAccountQuery query)
    {
        _query = query;
    }

    public Task<List<AccountViewModel>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
    {
        return _query.GetAccountsForManagementAsync(cancellationToken);
    }
}
