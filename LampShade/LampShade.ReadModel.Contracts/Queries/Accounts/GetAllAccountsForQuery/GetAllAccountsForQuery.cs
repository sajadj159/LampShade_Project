using MediatR;
using LampShade.ReadModel.Contracts.Account;

namespace LampShade.ReadModel.Contracts.Queries.Accounts.GetAllAccountsForQuery;

public class GetAllAccountsForQuery : IRequest<List<AccountQueryModel>> { }
