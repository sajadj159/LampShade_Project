using MediatR;
using _01_LampShadeQuery.Contract.Account;

namespace _01_LampShadeQuery.Features.Accounts.Queries.GetAllAccountsForQuery;

public class GetAllAccountsForQuery : IRequest<List<AccountQueryModel>> { }

public class GetAllAccountsForQueryHandler : IRequestHandler<GetAllAccountsForQuery, List<AccountQueryModel>>
{
    private readonly IAccountQuery _query;
    public GetAllAccountsForQueryHandler(IAccountQuery query) => _query = query;
    public Task<List<AccountQueryModel>> Handle(GetAllAccountsForQuery r, CancellationToken c) => Task.FromResult(_query.GetAccounts());
}
