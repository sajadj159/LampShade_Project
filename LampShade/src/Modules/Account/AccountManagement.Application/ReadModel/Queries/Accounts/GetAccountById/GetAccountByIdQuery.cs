using AccountManagement.Application.ReadModel.Accounts.Dto;
using MediatR;

namespace AccountManagement.Application.ReadModel.Queries.Accounts.GetAccountById;

public class GetAccountByIdQuery : IRequest<AccountViewModel>
{
    public long Id { get; set; }
}

