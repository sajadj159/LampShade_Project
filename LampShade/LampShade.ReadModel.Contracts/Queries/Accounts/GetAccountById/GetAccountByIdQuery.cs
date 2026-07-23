using LampShade.ReadModel.Contracts.Accounts.Dto;
using MediatR;

namespace LampShade.ReadModel.Contracts.Queries.Accounts.GetAccountById;

public class GetAccountByIdQuery : IRequest<AccountViewModel>
{
    public long Id { get; set; }
}

