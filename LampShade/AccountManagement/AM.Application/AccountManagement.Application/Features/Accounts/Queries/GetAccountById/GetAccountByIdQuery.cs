using AccountManagement.Application.Contracts.AC.Account;
using MediatR;

namespace AccountManagement.Application.Features.Accounts.Queries.GetAccountById;

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

    public Task<AccountViewModel> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_accountApplication.GetAccountBy(request.Id));
    }
}
