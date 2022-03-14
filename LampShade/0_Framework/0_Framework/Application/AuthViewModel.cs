using System.Collections.Generic;

namespace _0_Framework.Application
{
    public class AuthViewModel
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public long RoleId { get; set; }
        public string Role { get; set; }
        public string Mobile { get; set; }
        public List<int> Permissions { get; set; }

        public AuthViewModel()
        {
        }
        public AuthViewModel(long id, string username, string fullname, string mobile, long roleId, List<int> permissions)
        {
            Id = id;
            Username = username;
            Fullname = fullname;
            Mobile = mobile;
            RoleId = roleId;
            Permissions = permissions;
        }
    }
}