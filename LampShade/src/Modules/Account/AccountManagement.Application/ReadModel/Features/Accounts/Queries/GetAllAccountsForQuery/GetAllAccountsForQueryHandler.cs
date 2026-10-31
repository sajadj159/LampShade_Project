using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AccountManagement.Application.ReadModel.Account;

using QueryRequest = AccountManagement.Application.ReadModel.Queries.Accounts.GetAllAccountsForQuery.GetAllAccountsForQuery;

namespace AccountManagement.Application.ReadModel.Features.Accounts.Queries.GetAllAccountsForQuery;



public class GetAllAccountsForQueryHandler : IRequestHandler<QueryRequest, List<AccountQueryModel>>
{
    private readonly IAccountQuery _query;
    public GetAllAccountsForQueryHandler(IAccountQuery query) => _query = query;
    public Task<List<AccountQueryModel>> Handle(QueryRequest r, CancellationToken c) => _query.GetAccountsAsync(c);
}
