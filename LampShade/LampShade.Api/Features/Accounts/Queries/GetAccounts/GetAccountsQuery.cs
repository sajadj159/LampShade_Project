using AccountManagement.Application.Contracts.AC.Account;
using MediatR;

namespace LampShade.Api.Features.Accounts.Queries.GetAccounts;

public class GetAccountsQuery : IRequest<List<AccountViewModel>> { }

public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, List<AccountViewModel>>
{
    private readonly IAccountApplication _accountApplication;

    public GetAccountsQueryHandler(IAccountApplication accountApplication)
    {
        _accountApplication = accountApplication;
    }

    public async Task<List<AccountViewModel>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_accountApplication.GetAccounts());
    }
}
