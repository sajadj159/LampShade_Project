using InventoryManagement.Application.Contract.AC.Inventory;
using MediatR;

namespace LampShade.Api.Features.Inventories.Queries.SearchInventories;

public class SearchInventoriesQuery : IRequest<List<InventoryViewModel>>
{
    public long? ProductId { get; set; }
    public bool? InStock { get; set; }
}

public class SearchInventoriesQueryHandler : IRequestHandler<SearchInventoriesQuery, List<InventoryViewModel>>
{
    private readonly IInventoryApplication _application;

    public SearchInventoriesQueryHandler(IInventoryApplication application)
    {
        _application = application;
    }

    public async Task<List<InventoryViewModel>> Handle(SearchInventoriesQuery request, CancellationToken cancellationToken)
    {
        var searchModel = new InventorySearchModel
        {
            ProductId = request.ProductId ?? 0,
            InStock = request.InStock ?? false
        };
        return await Task.FromResult(_application.Search(searchModel));
    }
}

