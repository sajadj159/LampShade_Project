using System.Threading;
using System.Threading.Tasks;
#nullable enable

using InventoryManagement.Application.Contract.AC.Inventory;
using MediatR;
using InventoryManagement.Application.ReadModel.Inventory;

using InventoryManagement.Application.ReadModel.Queries.Inventories.SearchInventories;

namespace InventoryManagement.Application.ReadModel.Features.Inventories.Queries.SearchInventories;



public class SearchInventoriesQueryHandler : IRequestHandler<SearchInventoriesQuery, List<InventoryViewModel>>
{
    private readonly IInventoryQuery _repository;

    public SearchInventoriesQueryHandler(IInventoryQuery repository)
    {
        _repository = repository;
    }

    public Task<List<InventoryViewModel>> Handle(SearchInventoriesQuery request, CancellationToken cancellationToken)
    {
        var searchModel = new InventorySearchModel
        {
            ProductId = request.ProductId ?? 0,
            InStock = request.InStock ?? false
        };
        return _repository.SearchAsync(searchModel, cancellationToken);
    }
}
