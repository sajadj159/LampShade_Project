using LampShade.ReadModel.Contracts.Accounts.Dto;
using MediatR;

namespace LampShade.ReadModel.Contracts.Queries.Accounts.GetAccounts;

public class GetAccountsQuery : IRequest<List<AccountViewModel>> { }

