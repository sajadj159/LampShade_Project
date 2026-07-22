using AccountManagement.Application.Contracts.AC.Account;
using MediatR;

namespace AccountManagement.Application.Features.Accounts.Queries.GetAccounts;

public class GetAccountsQuery : IRequest<List<AccountViewModel>> { }

public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, List<AccountViewModel>>
{
    private readonly IAccountApplication _accountApplication;

    public GetAccountsQueryHandler(IAccountApplication accountApplication)
    {
        _accountApplication = accountApplication;
    }

    public Task<List<AccountViewModel>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_accountApplication.GetAccounts());
    }
}
