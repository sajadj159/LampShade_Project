using InventoryManagement.Application.Contract.AC.Inventory;
using MediatR;
using _0_Framework.Application;

using InventoryManagement.Application.Contracts.Commands.Inventories.IncreaseInventory;

namespace InventoryManagement.Application.Features.Inventories.Commands.IncreaseInventory;



public class IncreaseInventoryCommandHandler : IRequestHandler<IncreaseInventoryCommand, OperationResult>
{
    private readonly IInventoryApplication _application;

    public IncreaseInventoryCommandHandler(IInventoryApplication application)
    {
        _application = application;
    }

    public Task<OperationResult> Handle(IncreaseInventoryCommand request, CancellationToken cancellationToken)
    {
        var command = new InventoryManagement.Application.Contract.AC.Inventory.IncreaseInventory
        {
            InventoryId = request.InventoryId,
            Count = request.Count,
            Description = request.Description
        };
        return Task.FromResult(_application.Increase(command));
    }
}
