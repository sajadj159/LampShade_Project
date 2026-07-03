using InventoryManagement.Application.Contract.AC.Inventory;
using MediatR;

namespace LampShade.Api.Features.Inventories.Queries.GetInventoryById;

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

    public async Task<EditInventory> Handle(GetInventoryByIdQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_application.GetDetails(request.Id));
    }
}
