using MediatR;
using _01_LampShadeQuery.Contract.Account;

namespace LampShade.Api.Features.Accounts.Queries.GetAllAccountsForQuery;

public class GetAllAccountsForQuery : IRequest<List<AccountQueryModel>> { }

public class GetAllAccountsForQueryHandler : IRequestHandler<GetAllAccountsForQuery, List<AccountQueryModel>>
{
    private readonly IAccountQuery _query;
    public GetAllAccountsForQueryHandler(IAccountQuery query) => _query = query;
    public async Task<List<AccountQueryModel>> Handle(GetAllAccountsForQuery r, CancellationToken c) => await Task.FromResult(_query.GetAccounts());
}
