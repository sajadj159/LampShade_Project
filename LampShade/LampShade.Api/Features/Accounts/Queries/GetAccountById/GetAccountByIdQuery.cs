using AccountManagement.Application.Contracts.AC.Account;
using MediatR;

namespace LampShade.Api.Features.Accounts.Queries.GetAccountById;

public class GetAccountByIdQuery : IRequest<AccountViewModel>
{
    public long Id { get; set; }
}

public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountViewModel>
{
    private readonly IAccountApplication _accountApplication;

    public GetAccountByIdQueryHandler(IAccountApplication accountApplication)
    {
        _accountApplication = accountApplication;
    }

    public async Task<AccountViewModel> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_accountApplication.GetAccountBy(request.Id));
    }
}
