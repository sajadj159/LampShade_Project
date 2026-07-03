using InventoryManagement.Application.Contract.AC.Inventory;
using MediatR;
using _0_Framework.Application;

namespace LampShade.Api.Features.Inventories.Commands.CreateInventory;

public class CreateInventoryCommand : IRequest<OperationResult>
{
    public long ProductId { get; set; }
    public double UnitPrice { get; set; }
}

public class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, OperationResult>
{
    private readonly IInventoryApplication _application;

    public CreateInventoryCommandHandler(IInventoryApplication application)
    {
        _application = application;
    }

    public async Task<OperationResult> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
    {
        var command = new InventoryManagement.Application.Contract.AC.Inventory.CreateInventory
        {
            ProductId = request.ProductId,
            UnitPrice = request.UnitPrice
        };
        return await Task.FromResult(_application.Create(command));
    }
}
