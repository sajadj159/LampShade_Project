namespace InventoryManagement.Application.Contract.AC.Inventory
{
    public class ReduceInventory
    {
        public long InventoryId { get; set; }
        public long ProductId { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
        public long OrderId { get; set; }

        public ReduceInventory()
        {
        }

        public ReduceInventory(long productId, int count, string description, long orderId)
        {
            ProductId = productId;
            Count = count;
            Description = description;
            OrderId = orderId;
        }
    }
}