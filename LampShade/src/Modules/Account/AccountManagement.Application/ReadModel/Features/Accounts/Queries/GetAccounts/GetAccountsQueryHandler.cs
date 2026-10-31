using System.Threading;
using System.Threading.Tasks;
using AccountManagement.Application.ReadModel.Account;
using AccountManagement.Application.ReadModel.Accounts.Dto;
using MediatR;

using AccountManagement.Application.ReadModel.Queries.Accounts.GetAccounts;

namespace AccountManagement.Application.ReadModel.Features.Accounts.Queries.GetAccounts;



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
