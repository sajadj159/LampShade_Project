using _0_Framework.Domain;

namespace AccountManagement.Domain.AccountAgg
{
    public class Account : EntityBase
    {
        public string UserName { get; private set; }
        public string FullName { get; private set; }
        public string Password { get; private set; }
        public string Mobile { get; private set; }
        public long RoleId { get; private set; }
        public string ProfilePhoto { get; private set; }

        protected Account()
        {
        }
        public Account(string userName, string fullName, string password, string mobile, long roleId, string profilePhoto)
        {
            UserName = userName;
            FullName = fullName;
            Password = password;
            Mobile = mobile;
            RoleId = roleId;
            ProfilePhoto = profilePhoto;
        }


        public void Edit(string userName, string fullName, string mobile, long roleId, string profilePhoto)
        {
            UserName = userName;
            FullName = fullName;
            Mobile = mobile;
            RoleId = roleId;
            if (!string.IsNullOrWhiteSpace(profilePhoto))
            {
                ProfilePhoto = profilePhoto;
            }
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }
    }
}