using System.Threading;
using System.Threading.Tasks;
using AccountManagement.Application.ReadModel.Account;
using AccountManagement.Application.ReadModel.Accounts.Dto;
using MediatR;

using AccountManagement.Application.ReadModel.Queries.Accounts.GetAccountById;

namespace AccountManagement.Application.ReadModel.Features.Accounts.Queries.GetAccountById;



public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountViewModel>
{
    private readonly IAccountQuery _query;

    public GetAccountByIdQueryHandler(IAccountQuery query)
    {
        _query = query;
    }

    public Task<AccountViewModel> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        return _query.GetAccountAsync(request.Id, cancellationToken);
    }
}
