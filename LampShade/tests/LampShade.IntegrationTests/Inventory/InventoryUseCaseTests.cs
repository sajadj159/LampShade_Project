using AwesomeAssertions;
using InventoryManagement.Application.Contracts.Commands.Inventories.CreateInventory;
using InventoryManagement.Application.Contracts.Commands.Inventories.EditInventory;
using InventoryManagement.Application.Contracts.Commands.Inventories.IncreaseInventory;
using InventoryManagement.Application.Contracts.Commands.Inventories.ReduceInventory;
using InventoryManagement.Application.Contracts.Commands.Inventories.ReduceInventoryRange;
using InventoryManagement.Application.ReadModel.Queries.Inventories.CheckStock;
using InventoryManagement.Application.ReadModel.Queries.Inventories.GetInventoryOperations;
using InventoryManagement.Application.ReadModel.Queries.Inventories.SearchInventories;
using InventoryManagement.Infrastructure.EFCore;
using LampShade.BuildingBlocks.Application;
using LampShade.BuildingBlocks.Domain;
using LampShade.IntegrationTests.Fixtures;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LampShade.IntegrationTests.Inventory;

[Collection(nameof(ModularMonolithCollection))]
public class InventoryUseCaseTests(ModularMonolithFixture app)
{
    private CancellationToken CancellationToken => TestContext.Current.CancellationToken;

    [Fact]
    public async Task CreateInventory_PersistsInventory_AndRejectsASecondOneForTheSameProduct()
    {
        var productId = Random.Shared.NextInt64(1, long.MaxValue);

        var first = await app.SendAsync(new CreateInventoryCommand { ProductId = productId, UnitPrice = 150_000 });
        var second = await app.SendAsync(new CreateInventoryCommand { ProductId = productId, UnitPrice = 175_000 });

        first.IsSucceeded.Should().BeTrue();
        second.IsSucceeded.Should().BeFalse();
        var inventories = await app.WithAsync<InventoryContext, List<InventoryManagement.Domain.InventoryAgg.Inventory>>(c => c.Inventory.Where(x => x.ProductId == productId).ToListAsync(CancellationToken));
        inventories.Should().ContainSingle().Which.UnitPrice.Should().Be(150_000);
    }

    [Fact]
    public async Task EditIncreaseAndReduce_ChangeStockAndRecordOperations()
    {
        var inventory = await app.StockAsync(Random.Shared.NextInt64(1, long.MaxValue), 100_000, 0);
        var newProductId = Random.Shared.NextInt64(1, long.MaxValue);
        app.Auth.SignInAs(1, Roles.Administrator);

        var edit = await app.SendAsync(new EditInventoryCommand { Id = inventory.Id, ProductId = newProductId, UnitPrice = 175_000 });
        var increase = await app.SendAsync(new IncreaseInventoryCommand { InventoryId = inventory.Id, Count = 10, Description = "Restock" });
        var reduce = await app.SendAsync(new ReduceInventoryCommand { InventoryId = inventory.Id, Count = 4, Description = "Order", OrderId = 99 });
        var range = await app.SendAsync(new ReduceInventoryRangeCommand { Items = [new ReduceInventoryItem { ProductId = newProductId, Count = 1, Description = "Batch", OrderId = 100 }] });
        var operations = await app.SendAsync(new GetInventoryOperationsQuery { InventoryId = inventory.Id });

        new[] { edit, increase, reduce, range }.Should().AllSatisfy(result => result.IsSucceeded.Should().BeTrue());
        var persisted = await app.WithAsync<InventoryContext, InventoryManagement.Domain.InventoryAgg.Inventory>(c => c.Inventory.SingleAsync(x => x.Id == inventory.Id, CancellationToken));
        persisted.ProductId.Should().Be(newProductId);
        persisted.UnitPrice.Should().Be(175_000);
        persisted.CalculateCurrentCount().Should().Be(5);
        operations.Should().HaveCount(3).And.Contain(x => x.OrderId == 99 && !x.Operation);
    }

    [Fact]
    public async Task Reduce_BeyondAvailableStock_IsRejectedByTheAggregate()
    {
        var inventory = await app.StockAsync(Random.Shared.NextInt64(1, long.MaxValue), 100_000, 2);
        app.Auth.SignInAs(1, Roles.Administrator);

        var reduce = () => app.SendAsync(new ReduceInventoryCommand { InventoryId = inventory.Id, Count = 3, Description = "Too many", OrderId = 1 });

        await reduce.Should().ThrowAsync<DomainException>();
        var persisted = await app.WithAsync<InventoryContext, InventoryManagement.Domain.InventoryAgg.Inventory>(c => c.Inventory.SingleAsync(x => x.Id == inventory.Id, CancellationToken));
        persisted.CalculateCurrentCount().Should().Be(2);
    }

    [Fact]
    public async Task SearchAndCheckStock_NameProductsThroughTheShopModuleApi()
    {
        var product = await app.ProductAsync();
        await app.StockAsync(product.Id, 120_000, 3);

        var search = await app.SendAsync(new SearchInventoriesQuery { ProductId = product.Id });
        var enough = await app.SendAsync(new CheckStockQuery { ProductId = product.Id, Count = 3 });
        var tooMany = await app.SendAsync(new CheckStockQuery { ProductId = product.Id, Count = 4 });

        search.Should().ContainSingle().Which.Product.Should().Be(product.Name);
        enough.IsStock.Should().BeTrue();
        tooMany.IsStock.Should().BeFalse();
        tooMany.ProductName.Should().Be(product.Name);
    }
}
