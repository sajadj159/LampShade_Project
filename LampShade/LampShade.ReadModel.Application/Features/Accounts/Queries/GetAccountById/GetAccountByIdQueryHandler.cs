using AccountManagement.Application.Contracts.AC.Account;
using MediatR;

using LampShade.ReadModel.Contracts.Queries.Accounts.GetAccountById;

namespace LampShade.ReadModel.Application.Features.Accounts.Queries.GetAccountById;



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
