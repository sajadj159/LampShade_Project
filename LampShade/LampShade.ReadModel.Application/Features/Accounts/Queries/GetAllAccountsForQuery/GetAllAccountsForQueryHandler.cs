using MediatR;
using LampShade.ReadModel.Contracts.Account;

using QueryRequest = LampShade.ReadModel.Contracts.Queries.Accounts.GetAllAccountsForQuery.GetAllAccountsForQuery;

namespace LampShade.ReadModel.Application.Features.Accounts.Queries.GetAllAccountsForQuery;



public class GetAllAccountsForQueryHandler : IRequestHandler<QueryRequest, List<AccountQueryModel>>
{
    private readonly IAccountQuery _query;
    public GetAllAccountsForQueryHandler(IAccountQuery query) => _query = query;
    public Task<List<AccountQueryModel>> Handle(QueryRequest r, CancellationToken c) => Task.FromResult(_query.GetAccounts());
}



