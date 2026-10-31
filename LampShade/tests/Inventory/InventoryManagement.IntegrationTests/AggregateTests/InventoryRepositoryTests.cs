using AccountManagement.Infrastructure.EFCore;
using AwesomeAssertions;
using InventoryManagement.Application.Contract.AC.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore.Repository;
using InventoryManagement.IntegrationTests.Fixtures;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;
using Xunit;

namespace InventoryManagement.IntegrationTests.AggregateTests;

[Collection(nameof(PostgreSqlCollection))]
public class InventoryRepositoryTests(PostgreSqlFixture database)
{
    [Fact]
    public async Task GetDetailsGetByAndSearch_ReturnPersistedInventory()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await using var inventoryContext = CreateInventoryContext();
        await using var shopContext = CreateShopContext();
        await using var accountContext = CreateAccountContext();
        await inventoryContext.Database.MigrateAsync(cancellationToken);
        await shopContext.Database.MigrateAsync(cancellationToken);
        var productId = Random.Shared.NextInt64(1, long.MaxValue);
        var inventory = new Inventory(productId, 120_000);
        inventory.Increase(4, 1, "Seed");
        inventoryContext.Inventory.Add(inventory);
        await inventoryContext.SaveChangesAsync(cancellationToken);
        var repository = new InventoryRepository(inventoryContext, shopContext, accountContext);

        var details = repository.GetDetails(inventory.Id);
        var byProduct = repository.GetBy(productId);
        var byProductAsync = await repository.GetByAsync(productId, cancellationToken);
        var search = repository.Search(new InventorySearchModel { ProductId = productId });

        details.Id.Should().Be(inventory.Id);
        details.UnitPrice.Should().Be(120_000);
        byProduct.Id.Should().Be(inventory.Id);
        byProductAsync.Id.Should().Be(inventory.Id);
        search.Should().ContainSingle(item => item.Id == inventory.Id && item.CurrentCount == 4);
    }

    [Fact]
    public async Task GetOperationLog_ReturnsPersistedInventoryOperations()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await using var inventoryContext = CreateInventoryContext();
        await using var shopContext = CreateShopContext();
        await using var accountContext = CreateAccountContext();
        await inventoryContext.Database.MigrateAsync(cancellationToken);
        await accountContext.Database.MigrateAsync(cancellationToken);
        var inventory = new Inventory(Random.Shared.NextInt64(1, long.MaxValue), 120_000);
        inventory.Increase(4, 1, "Seed");
        inventoryContext.Inventory.Add(inventory);
        await inventoryContext.SaveChangesAsync(cancellationToken);
        inventoryContext.ChangeTracker.Clear();
        var repository = new InventoryRepository(inventoryContext, shopContext, accountContext);

        var operations = repository.GetOperationLog(inventory.Id);

        operations.Should().ContainSingle(item => item.Count == 4 && item.Operation);
    }

    private InventoryContext CreateInventoryContext() => new(new DbContextOptionsBuilder<InventoryContext>().UseNpgsql(database.ConnectionString).Options);
    private ShopContext CreateShopContext() => new(new DbContextOptionsBuilder<ShopContext>().UseNpgsql(database.ConnectionString).Options);
    private AccountContext CreateAccountContext() => new(new DbContextOptionsBuilder<AccountContext>().UseNpgsql(database.ConnectionString).Options);
}