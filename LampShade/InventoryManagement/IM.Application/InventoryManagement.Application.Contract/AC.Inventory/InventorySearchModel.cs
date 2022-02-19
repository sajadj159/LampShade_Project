namespace InventoryManagement.Application.Contract.AC.Inventory
{
    public class InventorySearchModel
    {
        public long ProductId { get; set; }
        public bool InStock { get; set; }
    }
}