using Microsoft.AspNetCore.Http;

namespace AccountManagement.Application.Contracts.AC.Account
{
    public class CreateAccount
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public long RoleId { get; set; }
        public IFormFile ProfilePhoto { get; set; }
    }
}