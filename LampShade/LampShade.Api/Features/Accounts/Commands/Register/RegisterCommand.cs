using AccountManagement.Application.Contracts.AC.Account;
using MediatR;
using _0_Framework.Application;

namespace LampShade.Api.Features.Accounts.Commands.Register;

public class RegisterCommand : IRequest<OperationResult>
{
    public string UserName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public long RoleId { get; set; }
    public IFormFile? ProfilePhoto { get; set; }
}

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, OperationResult>
{
    private readonly IAccountApplication _accountApplication;

    public RegisterCommandHandler(IAccountApplication accountApplication)
    {
        _accountApplication = accountApplication;
    }

    public async Task<OperationResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var command = new AccountManagement.Application.Contracts.AC.Account.RegisterAccount
        {
            UserName = request.UserName,
            FullName = request.FullName,
            Password = request.Password,
            Mobile = request.Mobile,
            Address = request.Address,
            PostalCode = request.PostalCode,
            RoleId = request.RoleId,
            ProfilePhoto = request.ProfilePhoto
        };
        return await Task.FromResult(_accountApplication.Register(command));
    }
}
