using InventoryManagement.Application.Contract.AC.Inventory;
using MediatR;
using _0_Framework.Application;

using InventoryManagement.Application.Contracts.Commands.Inventories.EditInventory;

namespace InventoryManagement.Application.Features.Inventories.Commands.EditInventory;



public class EditInventoryCommandHandler : IRequestHandler<EditInventoryCommand, OperationResult>
{
    private readonly IInventoryApplication _application;

    public EditInventoryCommandHandler(IInventoryApplication application)
    {
        _application = application;
    }

    public Task<OperationResult> Handle(EditInventoryCommand request, CancellationToken cancellationToken)
    {
        var command = new InventoryManagement.Application.Contract.AC.Inventory.EditInventory
        {
            Id = request.Id,
            ProductId = request.ProductId,
            UnitPrice = request.UnitPrice
        };
        return Task.FromResult(_application.Edit(command));
    }
}
