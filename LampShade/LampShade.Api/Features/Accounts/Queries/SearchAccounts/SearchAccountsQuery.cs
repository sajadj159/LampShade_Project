using AccountManagement.Application.Contracts.AC.Account;
using MediatR;

namespace LampShade.Api.Features.Accounts.Queries.SearchAccounts;

public class SearchAccountsQuery : IRequest<List<AccountViewModel>>
{
    public string? FullName { get; set; }
    public string? UserName { get; set; }
    public string? Mobile { get; set; }
    public long? RoleId { get; set; }
}

public class SearchAccountsQueryHandler : IRequestHandler<SearchAccountsQuery, List<AccountViewModel>>
{
    private readonly IAccountApplication _accountApplication;

    public SearchAccountsQueryHandler(IAccountApplication accountApplication)
    {
        _accountApplication = accountApplication;
    }

    public async Task<List<AccountViewModel>> Handle(SearchAccountsQuery request, CancellationToken cancellationToken)
    {
        var searchModel = new AccountSearchModel
        {
            FullName = request.FullName,
            UserName = request.UserName,
            Mobile = request.Mobile,
            RoleId = request.RoleId ?? 0
        };
        return await Task.FromResult(_accountApplication.Search(searchModel));
    }
}
