namespace InventoryManagement.Application.Contract.AC.Inventory
{
    public class IncreaseInventory
    {
        public long InventoryId { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
    }
}