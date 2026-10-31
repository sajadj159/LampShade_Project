using System.Collections.Generic;
using LampShade.BuildingBlocks.Application;

namespace LampShade.BuildingBlocks.Application
{
    public interface IPermissionExposer
    {
        Dictionary<string, List<PermissionDto>> Expose();
    }
}