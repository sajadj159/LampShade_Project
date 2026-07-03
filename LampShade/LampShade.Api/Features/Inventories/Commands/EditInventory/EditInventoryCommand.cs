using InventoryManagement.Application.Contract.AC.Inventory;
using MediatR;
using _0_Framework.Application;

namespace LampShade.Api.Features.Inventories.Commands.EditInventory;

public class EditInventoryCommand : IRequest<OperationResult>
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public double UnitPrice { get; set; }
}

public class EditInventoryCommandHandler : IRequestHandler<EditInventoryCommand, OperationResult>
{
    private readonly IInventoryApplication _application;

    public EditInventoryCommandHandler(IInventoryApplication application)
    {
        _application = application;
    }

    public async Task<OperationResult> Handle(EditInventoryCommand request, CancellationToken cancellationToken)
    {
        var command = new InventoryManagement.Application.Contract.AC.Inventory.EditInventory
        {
            Id = request.Id,
            ProductId = request.ProductId,
            UnitPrice = request.UnitPrice
        };
        return await Task.FromResult(_application.Edit(command));
    }
}
