#nullable enable

using AccountManagement.Application.Contracts.AC.Account;
using MediatR;
using Microsoft.AspNetCore.Http;
using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.Commands.Accounts.Edit;

public class EditAccountCommand : ICommand<OperationResult>
{
    public long Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public long RoleId { get; set; }
    public IFormFile? ProfilePhoto { get; set; }
}
