using System.Collections.Generic;
using _0_Framework.Application;

namespace _0_Framework.Repository
{
    public interface IPermissionExposer
    {
        Dictionary<string, List<PermissionDto>> Expose();
    }
}