using System.Collections.Generic;

namespace LampShade.BuildingBlocks.Application
{
    public interface IAuthHelper
    {
        bool IsAuthenticated();
        void Signin(AuthViewModel account);
        void SignOut();
        string CurrentAccountRole();
        AuthViewModel CurrentAccountInfo();
        List<int> GetPermissions();
        long CurrentAccountId();
        string CurrentAccountMobile();
    }
}