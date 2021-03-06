using System.Collections.Generic;
using _0_Framework.Application;
using InventoryManagement.Application.Contract.AC.Inventory;
using InventoryManagement.Domain.InventoryAgg;

namespace InventoryManagement.Application
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IAuthHelper _authHelper;
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryApplication(IInventoryRepository inventoryRepository, IAuthHelper authHelper)
        {
            _inventoryRepository = inventoryRepository;
            _authHelper = authHelper;
        }

        public OperationResult Create(CreateInventory command)
        {
            var operationResult = new OperationResult();
            if (_inventoryRepository.Exist(x => x.ProductId == command.ProductId))
            {
                return operationResult.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var inventory = new Inventory(command.ProductId, command.UnitPrice);
            _inventoryRepository.Create(inventory);
            _inventoryRepository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditInventory command)
        {
            var operationResult = new OperationResult();
            var inventory = _inventoryRepository.Get(command.Id);
            if (inventory == null)
            {
                return operationResult.Failed(ApplicationMessages.RecordNotFound);
            }

            if (_inventoryRepository.Exist(x => x.ProductId == command.ProductId && x.Id != command.Id))
            {
                return operationResult.Failed(ApplicationMessages.DuplicatedRecord);
            }
            inventory.Edit(command.ProductId, command.UnitPrice);
            _inventoryRepository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Increase(IncreaseInventory command)
        {
            var operationResult = new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory == null)
            {
                return operationResult.Failed(ApplicationMessages.RecordNotFound);
            }

            var currentAccountId = _authHelper.CurrentAccountId();
            inventory.Increase(command.Count, currentAccountId, command.Description);
            _inventoryRepository.Save();
            return operationResult.Succeeded();

        }

        public OperationResult Reduce(ReduceInventory command)
        {
            var operationResult = new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory == null)
            {
                return operationResult.Failed(ApplicationMessages.RecordNotFound);
            }
            var currentAccountId = _authHelper.CurrentAccountId();
            inventory.Reduce(command.Count, currentAccountId, command.Description, 0);
            _inventoryRepository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Reduce(List<ReduceInventory> command)
        {
            var operationResult = new OperationResult();
            var currentAccountId = _authHelper.CurrentAccountId();
            foreach (var item in command)
            {
                var inventory = _inventoryRepository.GetBy(item.ProductId);
                inventory.Reduce(item.Count, currentAccountId, item.Description, item.OrderId);
            }
            _inventoryRepository.Save();
            return operationResult.Succeeded();
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            return _inventoryRepository.Search(searchModel);
        }

        public EditInventory GetDetails(long id)
        {
            return _inventoryRepository.GetDetails(id);
        }

        public List<InventoryOperationViewModel> GetOperationLog(long inventoryId)
        {
            return _inventoryRepository.GetOperationLog(inventoryId);
        }
    }
}