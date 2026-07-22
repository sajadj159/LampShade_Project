using AccountManagement.Application.Contracts.AC.Account;
using MediatR;

namespace LampShade.ReadModel.Contracts.Queries.Accounts.GetAccounts;

public class GetAccountsQuery : IRequest<List<AccountViewModel>> { }
