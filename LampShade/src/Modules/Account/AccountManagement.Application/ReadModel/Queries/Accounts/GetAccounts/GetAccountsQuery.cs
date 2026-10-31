using AccountManagement.Application.ReadModel.Accounts.Dto;
using MediatR;

namespace AccountManagement.Application.ReadModel.Queries.Accounts.GetAccounts;

public class GetAccountsQuery : IRequest<List<AccountViewModel>> { }

