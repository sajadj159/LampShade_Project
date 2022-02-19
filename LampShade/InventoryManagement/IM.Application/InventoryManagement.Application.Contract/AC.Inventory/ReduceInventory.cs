namespace InventoryManagement.Application.Contract.AC.Inventory
{
    public class ReduceInventory
    {
        public long ProductId { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
        public long OrderId { get; set; }

    }
}