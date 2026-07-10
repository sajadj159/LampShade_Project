namespace AccountManagement.Application.Contracts.AC.Account
{
    public class AccountViewModel
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Role { get; set; }
        public long RoleId { get; set; }
        public string ProfilePhoto { get; set; }
        public string CreationDate { get; set; }
    }
}