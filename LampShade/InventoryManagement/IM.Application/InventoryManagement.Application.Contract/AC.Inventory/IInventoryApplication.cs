using System.Collections.Generic;
using _0_Framework.Application;

namespace InventoryManagement.Application.Contract.AC.Inventory
{
    public interface IInventoryApplication
    {
        OperationResult Create(CreateInventory command);
        OperationResult Edit (EditInventory command);
        OperationResult Increase(IncreaseInventory command);
        OperationResult Reduce(ReduceInventory command);
        OperationResult Reduce(List<ReduceInventory> command);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
        EditInventory GetDetails(long id);
    }
}