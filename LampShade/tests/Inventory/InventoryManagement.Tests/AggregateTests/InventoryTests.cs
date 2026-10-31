using AwesomeAssertions;
using _0_Framework.Domain;
using InventoryManagement.Domain.InventoryAgg;
using Xunit;

namespace InventoryManagement.Tests.AggregateTests;

public class InventoryTests
{
    [Fact] public void Constructor_InheritsFromEntityBase() => CreateInventory().Should().BeAssignableTo<EntityBase>();
    [Fact] public void Constructor_SetsCreationDate() { var beforeCreation = DateTime.UtcNow; var inventory = CreateInventory(); inventory.CreationDate.Should().BeOnOrAfter(beforeCreation); inventory.CreationDate.Should().BeOnOrBefore(DateTime.UtcNow); }
    [Fact] public void Constructor_SetsDefaultId() => CreateInventory().Id.Should().Be(0);
    [Fact] public void Constructor_SetsProductId() => CreateInventory().ProductId.Should().Be(1);
    [Fact] public void Constructor_SetsUnitPrice() => CreateInventory().UnitPrice.Should().Be(100_000);
    [Fact] public void Constructor_SetsInStockToFalse() => CreateInventory().InStock.Should().BeFalse();
    [Fact] public void Constructor_InitializesInventoryOperations() { var inventory = CreateInventory(); inventory.InventoryOperations.Should().NotBeNull(); inventory.InventoryOperations.Should().BeEmpty(); }
    [Fact] public void Edit_UpdatesProductId() { var inventory = CreateInventory(); inventory.Edit(2, 120_000); inventory.ProductId.Should().Be(2); }
    [Fact] public void Edit_UpdatesUnitPrice() { var inventory = CreateInventory(); inventory.Edit(2, 120_000); inventory.UnitPrice.Should().Be(120_000); }
    [Fact] public void CalculateCurrentCount_WhenThereAreNoOperations_ReturnsZero() => CreateInventory().CalculateCurrentCount().Should().Be(0);
    [Fact]
    public void Increase_AddsIncreaseOperationAndMarksInventoryAsInStock()
    {
        var inventory = CreateInventory(); inventory.Increase(10, 7, "Initial stock");
        inventory.InStock.Should().BeTrue(); inventory.CalculateCurrentCount().Should().Be(10);
        var operation = inventory.InventoryOperations.Should().ContainSingle().Which;
        operation.Operation.Should().BeTrue(); operation.Count.Should().Be(10); operation.OperatorId.Should().Be(7); operation.CurrentCount.Should().Be(10); operation.Description.Should().Be("Initial stock"); operation.OrderId.Should().Be(0); operation.InventoryId.Should().Be(0);
    }
    [Fact]
    public void Reduce_AddsReduceOperationAndMarksInventoryAsOutOfStockWhenCountIsZero()
    {
        var inventory = CreateInventory(); inventory.Increase(10, 7, "Initial stock"); inventory.Reduce(10, 8, "Order checkout", 20);
        inventory.InStock.Should().BeFalse(); inventory.CalculateCurrentCount().Should().Be(0);
        var operation = inventory.InventoryOperations.Last();
        operation.Operation.Should().BeFalse(); operation.Count.Should().Be(10); operation.OperatorId.Should().Be(8); operation.CurrentCount.Should().Be(0); operation.Description.Should().Be("Order checkout"); operation.OrderId.Should().Be(20); operation.InventoryId.Should().Be(0);
    }
    [Fact]
    public void CalculateCurrentCount_WithIncreaseAndReduction_ReturnsNetCount()
    {
        var inventory = CreateInventory(); inventory.Increase(15, 7, "Initial stock"); inventory.Reduce(4, 8, "Order checkout", 20);
        inventory.CalculateCurrentCount().Should().Be(11);
    }
    private static Inventory CreateInventory() => new(1, 100_000);
}