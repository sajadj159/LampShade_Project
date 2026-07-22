using AccountManagement.Application.Contracts.AC.Account;
using MediatR;

namespace LampShade.ReadModel.Contracts.Queries.Accounts.GetAccountById;

public class GetAccountByIdQuery : IRequest<AccountViewModel>
{
    public long Id { get; set; }
}
