using System.Threading;
using System.Threading.Tasks;
#nullable enable

using InventoryManagement.Application.Contract.AC.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using MediatR;

using LampShade.ReadModel.Contracts.Queries.Inventories.SearchInventories;

namespace LampShade.ReadModel.Application.Features.Inventories.Queries.SearchInventories;



public class SearchInventoriesQueryHandler : IRequestHandler<SearchInventoriesQuery, List<InventoryViewModel>>
{
    private readonly IInventoryRepository _repository;

    public SearchInventoriesQueryHandler(IInventoryRepository repository)
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
        return Task.FromResult(_repository.Search(searchModel));
    }
}
