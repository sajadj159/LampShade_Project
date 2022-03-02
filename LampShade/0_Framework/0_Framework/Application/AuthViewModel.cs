namespace _0_Framework.Application
{
    public class AuthViewModel
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public long RoleId { get; set; }

        public AuthViewModel(long id, string username, string fullname, long roleId)
        {
            Id = id;
            Username = username;
            Fullname = fullname;
            RoleId = roleId;
        }
    }
}