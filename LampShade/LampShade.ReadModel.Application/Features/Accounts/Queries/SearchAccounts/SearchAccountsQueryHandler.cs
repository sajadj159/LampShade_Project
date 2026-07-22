#nullable enable

using AccountManagement.Application.Contracts.AC.Account;
using MediatR;

using LampShade.ReadModel.Contracts.Queries.Accounts.SearchAccounts;

namespace LampShade.ReadModel.Application.Features.Accounts.Queries.SearchAccounts;



public class SearchAccountsQueryHandler : IRequestHandler<SearchAccountsQuery, List<AccountViewModel>>
{
    private readonly IAccountApplication _accountApplication;

    public SearchAccountsQueryHandler(IAccountApplication accountApplication)
    {
        _accountApplication = accountApplication;
    }

    public Task<List<AccountViewModel>> Handle(SearchAccountsQuery request, CancellationToken cancellationToken)
    {
        var searchModel = new AccountSearchModel
        {
            FullName = request.FullName,
            UserName = request.UserName,
            Mobile = request.Mobile,
            RoleId = request.RoleId ?? 0
        };
        return Task.FromResult(_accountApplication.Search(searchModel));
    }
}
