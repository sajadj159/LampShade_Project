using InventoryManagement.Application.Contract.AC.Inventory;
using MediatR;

namespace InventoryManagement.Application.Features.Inventories.Queries.GetInventoryById;

public class GetInventoryByIdQuery : IRequest<EditInventory>
{
    public long Id { get; set; }
}

public class GetInventoryByIdQueryHandler : IRequestHandler<GetInventoryByIdQuery, EditInventory>
{
    private readonly IInventoryApplication _application;

    public GetInventoryByIdQueryHandler(IInventoryApplication application)
    {
        _application = application;
    }

    public Task<EditInventory> Handle(GetInventoryByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_application.GetDetails(request.Id));
    }
}
