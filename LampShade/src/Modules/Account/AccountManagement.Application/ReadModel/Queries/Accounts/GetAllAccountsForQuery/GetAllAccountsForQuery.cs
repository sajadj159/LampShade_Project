using MediatR;
using AccountManagement.Application.ReadModel.Account;

namespace AccountManagement.Application.ReadModel.Queries.Accounts.GetAllAccountsForQuery;

public class GetAllAccountsForQuery : IRequest<List<AccountQueryModel>> { }
