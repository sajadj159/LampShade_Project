using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LampShade.BuildingBlocks.Application;

namespace InventoryManagement.Application.Contract.AC.Inventory
{
    public class CreateInventory
    {
        [Range(1,100000,ErrorMessage = ValidationMessages.IsRequired)]
        public long ProductId { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        public double UnitPrice { get; set; }
    }
}