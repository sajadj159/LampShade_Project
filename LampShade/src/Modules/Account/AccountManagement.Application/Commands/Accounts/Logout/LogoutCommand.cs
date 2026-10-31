using MediatR;
using LampShade.BuildingBlocks.Application;

namespace AccountManagement.Application.Contracts.Commands.Accounts.Logout;

public class LogoutCommand : ICommand<OperationResult> { }
