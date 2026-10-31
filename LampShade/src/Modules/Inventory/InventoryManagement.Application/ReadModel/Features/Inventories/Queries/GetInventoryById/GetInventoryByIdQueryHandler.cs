using System.Threading;
using System.Threading.Tasks;
using InventoryManagement.Application.Contract.AC.Inventory;
using MediatR;
using InventoryManagement.Application.ReadModel.Inventory;

using InventoryManagement.Application.ReadModel.Queries.Inventories.GetInventoryById;

namespace InventoryManagement.Application.ReadModel.Features.Inventories.Queries.GetInventoryById;



public class GetInventoryByIdQueryHandler : IRequestHandler<GetInventoryByIdQuery, EditInventory>
{
    private readonly IInventoryQuery _repository;

    public GetInventoryByIdQueryHandler(IInventoryQuery repository)
    {
        _repository = repository;
    }

    public Task<EditInventory> Handle(GetInventoryByIdQuery request, CancellationToken cancellationToken)
    {
        return _repository.GetDetailsAsync(request.Id, cancellationToken);
    }
}
