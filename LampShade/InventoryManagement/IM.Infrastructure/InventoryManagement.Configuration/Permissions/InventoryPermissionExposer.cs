﻿using System.Collections.Generic;
using _0_Framework.Repository;

namespace InventoryManagement.Configuration.Permissions
{
    public class InventoryPermissionExposer:IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Inventory", new List<PermissionDto>
                    {
                        new PermissionDto(50, "ListInventory"),
                        new PermissionDto(51, "SearchInventory"),
                        new PermissionDto(52, "DefineInventory"),
                        new PermissionDto(53, "EditInventory"),
                    }
                }
            };
        }
    }
}