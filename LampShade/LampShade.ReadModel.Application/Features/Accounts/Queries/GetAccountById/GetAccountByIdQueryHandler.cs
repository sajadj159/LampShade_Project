using System.Threading;
using System.Threading.Tasks;
using LampShade.ReadModel.Contracts.Account;
using LampShade.ReadModel.Contracts.Accounts.Dto;
using MediatR;

using LampShade.ReadModel.Contracts.Queries.Accounts.GetAccountById;

namespace LampShade.ReadModel.Application.Features.Accounts.Queries.GetAccountById;



public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountViewModel>
{
    private readonly IAccountQuery _query;

    public GetAccountByIdQueryHandler(IAccountQuery query)
    {
        _query = query;
    }

    public Task<AccountViewModel> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_query.GetAccount(request.Id));
    }
}
