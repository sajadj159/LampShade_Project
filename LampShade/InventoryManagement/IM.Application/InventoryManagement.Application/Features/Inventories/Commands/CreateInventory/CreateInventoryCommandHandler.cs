using InventoryManagement.Application.Contract.AC.Inventory;
using MediatR;
using _0_Framework.Application;

using InventoryManagement.Application.Contracts.Commands.Inventories.CreateInventory;

namespace InventoryManagement.Application.Features.Inventories.Commands.CreateInventory;



public class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, OperationResult>
{
    private readonly IInventoryApplication _application;

    public CreateInventoryCommandHandler(IInventoryApplication application)
    {
        _application = application;
    }

    public Task<OperationResult> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
    {
        var command = new InventoryManagement.Application.Contract.AC.Inventory.CreateInventory
        {
            ProductId = request.ProductId,
            UnitPrice = request.UnitPrice
        };
        return Task.FromResult(_application.Create(command));
    }
}
