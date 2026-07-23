using InventoryManagement.Application.Contract.AC.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using MediatR;

using LampShade.ReadModel.Contracts.Queries.Inventories.GetInventoryById;

namespace LampShade.ReadModel.Application.Features.Inventories.Queries.GetInventoryById;



public class GetInventoryByIdQueryHandler : IRequestHandler<GetInventoryByIdQuery, EditInventory>
{
    private readonly IInventoryRepository _repository;

    public GetInventoryByIdQueryHandler(IInventoryRepository repository)
    {
        _repository = repository;
    }

    public Task<EditInventory> Handle(GetInventoryByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_repository.GetDetails(request.Id));
    }
}

