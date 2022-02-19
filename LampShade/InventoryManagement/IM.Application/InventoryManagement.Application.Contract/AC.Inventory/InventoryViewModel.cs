namespace InventoryManagement.Application.Contract.AC.Inventory
{
    public class InventoryViewModel
    {
        public long Id { get; set; }
        public string Product { get; set; }
        public int UnitPrice { get; set; }
        public bool InStock { get; set; }
        public long CurrentCount { get; set; }
    }
}