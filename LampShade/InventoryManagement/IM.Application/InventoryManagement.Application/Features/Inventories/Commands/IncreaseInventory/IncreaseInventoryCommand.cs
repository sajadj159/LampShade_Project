using InventoryManagement.Application.Contract.AC.Inventory;
using MediatR;
using _0_Framework.Application;

namespace InventoryManagement.Application.Features.Inventories.Commands.IncreaseInventory;

public class IncreaseInventoryCommand : IRequest<OperationResult>
{
    public long InventoryId { get; set; }
    public int Count { get; set; }
    public string Description { get; set; } = string.Empty;
}

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
