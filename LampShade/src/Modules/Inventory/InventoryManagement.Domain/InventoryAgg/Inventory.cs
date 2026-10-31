using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using LampShade.BuildingBlocks.Domain;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class Inventory : EntityBase
    {
        public long ProductId { get; private set; }
        public double UnitPrice { get; private set; }
        public bool InStock { get; private set; }
        public List<InventoryOperation> InventoryOperations { get; private set; }

        protected Inventory()
        {
            InventoryOperations = new List<InventoryOperation>();
        }

        public Inventory(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            InStock = false;
            InventoryOperations = new List<InventoryOperation>();
        }

        public void Edit(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
        }
        public long CalculateCurrentCount()
        {
            var plus = InventoryOperations.Where(x => x.Operation).Sum(x => x.Count);
            var minus = InventoryOperations.Where(x => !x.Operation).Sum(x => x.Count);
            return plus - minus;
        }

        public void Increase(long count, long operatorId, string description)
        {
            var currentCount = CalculateCurrentCount() + count;
            var operation = new InventoryOperation(true, count, operatorId, currentCount, description, 0, Id);
            InventoryOperations.Add(operation);
            InStock = currentCount > 0;
        }

        public void Reduce(long count, long operatorId, string description, long orderId)
        {
            if (count <= 0)
                throw new DomainException("The reduced count must be greater than zero.");

            var currentCount = CalculateCurrentCount() - count;
            if (currentCount < 0)
                throw new DomainException($"Not enough stock for product {ProductId}: {CalculateCurrentCount()} available, {count} requested.");

            var operation = new InventoryOperation(false,count,operatorId,currentCount,description,orderId,Id);
            InventoryOperations.Add(operation);
            InStock=currentCount > 0;

        }
    }
}