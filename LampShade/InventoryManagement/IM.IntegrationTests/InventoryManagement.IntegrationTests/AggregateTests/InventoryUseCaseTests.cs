using AccountManagement.Infrastructure.EFCore;
using AwesomeAssertions;
using InventoryManagement.Application.Features.Inventories.Commands.CreateInventory;
using InventoryManagement.Application.Features.Inventories.Commands.EditInventory;
using InventoryManagement.Application.Features.Inventories.Commands.IncreaseInventory;
using InventoryManagement.Application.Features.Inventories.Commands.ReduceInventory;
using InventoryManagement.Application.Features.Inventories.Commands.ReduceInventoryRange;
using InventoryManagement.Application.Contracts.Commands.Inventories.CreateInventory;
using InventoryManagement.Application.Contracts.Commands.Inventories.EditInventory;
using InventoryManagement.Application.Contracts.Commands.Inventories.IncreaseInventory;
using InventoryManagement.Application.Contracts.Commands.Inventories.ReduceInventory;
using InventoryManagement.Application.Contracts.Commands.Inventories.ReduceInventoryRange;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore.Repository;
using InventoryManagement.IntegrationTests.Fixtures;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace InventoryManagement.IntegrationTests.AggregateTests;

[Collection(nameof(PostgreSqlCollection))]
public class InventoryUseCaseTests(PostgreSqlFixture database)
{
    [Fact]
    public async Task CreateInventory_PersistsInventoryInPostgreSql()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await using var inventoryContext = CreateInventoryContext();
        await inventoryContext.Database.MigrateAsync(cancellationToken);
        await using var shopContext = CreateShopContext();
        await using var accountContext = CreateAccountContext();
        var repository = new InventoryRepository(inventoryContext, shopContext, accountContext);
        var productId = Random.Shared.NextInt64(1, long.MaxValue);

        var result = await new CreateInventoryCommandHandler(repository).Handle(new CreateInventoryCommand { ProductId = productId, UnitPrice = 150_000 }, cancellationToken);
        await inventoryContext.SaveChangesAsync(cancellationToken);

        result.IsSucceeded.Should().BeTrue();
        var inventory = await inventoryContext.Inventory.SingleAsync(item => item.ProductId == productId, cancellationToken);
        inventory.UnitPrice.Should().Be(150_000);
        inventory.InStock.Should().BeFalse();
    }

    [Fact]
    public async Task CreateInventory_WhenProductAlreadyHasInventory_ReturnsFailureWithoutAddingAnotherRecord()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await using var inventoryContext = CreateInventoryContext();
        await inventoryContext.Database.MigrateAsync(cancellationToken);
        await using var shopContext = CreateShopContext();
        await using var accountContext = CreateAccountContext();
        var repository = new InventoryRepository(inventoryContext, shopContext, accountContext);
        var productId = Random.Shared.NextInt64(1, long.MaxValue);
        inventoryContext.Inventory.Add(new Inventory(productId, 100_000));
        await inventoryContext.SaveChangesAsync(cancellationToken);

        var result = await new CreateInventoryCommandHandler(repository).Handle(new CreateInventoryCommand { ProductId = productId, UnitPrice = 150_000 }, cancellationToken);
        await inventoryContext.SaveChangesAsync(cancellationToken);

        result.IsSucceeded.Should().BeFalse();
        (await inventoryContext.Inventory.CountAsync(item => item.ProductId == productId, cancellationToken)).Should().Be(1);
    }

    [Fact]    public async Task EditInventory_ChangesProductAndUnitPrice()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await using var inventoryContext = CreateInventoryContext();
        await inventoryContext.Database.MigrateAsync(cancellationToken);
        await using var shopContext = CreateShopContext();
        await using var accountContext = CreateAccountContext();
        var inventory = await AddInventoryAsync(inventoryContext, cancellationToken);
        var repository = new InventoryRepository(inventoryContext, shopContext, accountContext);
        var newProductId = Random.Shared.NextInt64(1, long.MaxValue);

        var result = await new EditInventoryCommandHandler(repository).Handle(new EditInventoryCommand { Id = inventory.Id, ProductId = newProductId, UnitPrice = 175_000 }, cancellationToken);
        await inventoryContext.SaveChangesAsync(cancellationToken);
        inventoryContext.ChangeTracker.Clear();

        result.IsSucceeded.Should().BeTrue();
        var persisted = await inventoryContext.Inventory.SingleAsync(item => item.Id == inventory.Id, cancellationToken);
        persisted.ProductId.Should().Be(newProductId);
        persisted.UnitPrice.Should().Be(175_000);
    }

    [Fact]
    public async Task IncreaseInventory_AddsPositiveOperationAndMarksStockAvailable()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await using var inventoryContext = CreateInventoryContext();
        await inventoryContext.Database.MigrateAsync(cancellationToken);
        await using var shopContext = CreateShopContext();
        await using var accountContext = CreateAccountContext();
        var inventory = await AddInventoryAsync(inventoryContext, cancellationToken);
        var repository = new InventoryRepository(inventoryContext, shopContext, accountContext);

        var result = await new IncreaseInventoryCommandHandler(repository, new TestAuthHelper(42)).Handle(new IncreaseInventoryCommand { InventoryId = inventory.Id, Count = 8, Description = "Initial stock" }, cancellationToken);
        await inventoryContext.SaveChangesAsync(cancellationToken);
        inventoryContext.ChangeTracker.Clear();

        result.IsSucceeded.Should().BeTrue();
        var persisted = await inventoryContext.Inventory.Include(item => item.InventoryOperations).SingleAsync(item => item.Id == inventory.Id, cancellationToken);
        persisted.InStock.Should().BeTrue();
        persisted.CalculateCurrentCount().Should().Be(8);
        persisted.InventoryOperations.Should().ContainSingle(item => item.Operation && item.OperatorId == 42);
    }

    [Fact]
    public async Task ReduceInventory_AddsNegativeOperationForOrder()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await using var inventoryContext = CreateInventoryContext();
        await inventoryContext.Database.MigrateAsync(cancellationToken);
        await using var shopContext = CreateShopContext();
        await using var accountContext = CreateAccountContext();
        var inventory = await AddInventoryAsync(inventoryContext, cancellationToken, 10);
        var repository = new InventoryRepository(inventoryContext, shopContext, accountContext);

        var result = await new ReduceInventoryCommandHandler(repository, new TestAuthHelper(42)).Handle(new ReduceInventoryCommand { InventoryId = inventory.Id, ProductId = inventory.ProductId, Count = 3, Description = "Order dispatch", OrderId = 99 }, cancellationToken);
        await inventoryContext.SaveChangesAsync(cancellationToken);
        inventoryContext.ChangeTracker.Clear();

        result.IsSucceeded.Should().BeTrue();
        var persisted = await inventoryContext.Inventory.Include(item => item.InventoryOperations).SingleAsync(item => item.Id == inventory.Id, cancellationToken);
        persisted.CalculateCurrentCount().Should().Be(7);
        persisted.InventoryOperations.Should().Contain(item => !item.Operation && item.OrderId == 99 && item.Count == 3);
    }

    [Fact]
    public async Task ReduceInventoryRange_ReducesEveryRequestedInventory()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await using var inventoryContext = CreateInventoryContext();
        await inventoryContext.Database.MigrateAsync(cancellationToken);
        await using var shopContext = CreateShopContext();
        await using var accountContext = CreateAccountContext();
        var first = await AddInventoryAsync(inventoryContext, cancellationToken, 5);
        var second = await AddInventoryAsync(inventoryContext, cancellationToken, 6);
        var repository = new InventoryRepository(inventoryContext, shopContext, accountContext);
        var command = new ReduceInventoryRangeCommand { Items = [new ReduceInventoryItem { ProductId = first.ProductId, Count = 2, Description = "Order", OrderId = 100 }, new ReduceInventoryItem { ProductId = second.ProductId, Count = 3, Description = "Order", OrderId = 100 }] };

        var result = await new ReduceInventoryRangeCommandHandler(repository, new TestAuthHelper()).Handle(command, cancellationToken);
        await inventoryContext.SaveChangesAsync(cancellationToken);
        inventoryContext.ChangeTracker.Clear();

        result.IsSucceeded.Should().BeTrue();
        (await inventoryContext.Inventory.Include(item => item.InventoryOperations).SingleAsync(item => item.Id == first.Id, cancellationToken)).CalculateCurrentCount().Should().Be(3);
        (await inventoryContext.Inventory.Include(item => item.InventoryOperations).SingleAsync(item => item.Id == second.Id, cancellationToken)).CalculateCurrentCount().Should().Be(3);
    }

    private InventoryContext CreateInventoryContext() => new(new DbContextOptionsBuilder<InventoryContext>().UseNpgsql(database.ConnectionString).Options);
    private ShopManagement.Infrastructure.EFCore.ShopContext CreateShopContext() => new(new DbContextOptionsBuilder<ShopManagement.Infrastructure.EFCore.ShopContext>().UseNpgsql(database.ConnectionString).Options);
    private AccountContext CreateAccountContext() => new(new DbContextOptionsBuilder<AccountContext>().UseNpgsql(database.ConnectionString).Options);
    private static async Task<Inventory> AddInventoryAsync(InventoryContext context, CancellationToken cancellationToken, long initialCount = 0)
    {
        var inventory = new Inventory(Random.Shared.NextInt64(1, long.MaxValue), 100_000);
        if (initialCount > 0) inventory.Increase(initialCount, 1, "Seed");
        context.Inventory.Add(inventory);
        await context.SaveChangesAsync(cancellationToken);
        return inventory;
    }
}