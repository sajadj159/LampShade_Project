namespace LampShade.ReadModel.Contracts.Accounts.Dto;

public class AccountViewModel
{
    public long Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public long RoleId { get; set; }
    public string ProfilePhoto { get; set; } = string.Empty;
    public string CreationDate { get; set; } = string.Empty;
}
