using AwesomeAssertions;
using InventoryManagement.Domain.InventoryAgg;
using Xunit;

namespace InventoryManagement.Tests.AggregateTests;

public class InventoryOperationTests
{
    [Fact] public void Constructor_SetsDefaultId() => CreateOperation().Id.Should().Be(0);
    [Fact] public void Constructor_SetsOperation() => CreateOperation().Operation.Should().BeTrue();
    [Fact] public void Constructor_SetsCount() => CreateOperation().Count.Should().Be(10);
    [Fact] public void Constructor_SetsOperatorId() => CreateOperation().OperatorId.Should().Be(7);
    [Fact]
    public void Constructor_SetsOperationDate()
    {
        var beforeCreation = DateTime.UtcNow; var operation = CreateOperation();
        operation.OperationDate.Should().BeOnOrAfter(beforeCreation); operation.OperationDate.Should().BeOnOrBefore(DateTime.UtcNow);
    }
    [Fact] public void Constructor_SetsCurrentCount() => CreateOperation().CurrentCount.Should().Be(10);
    [Fact] public void Constructor_SetsDescription() => CreateOperation().Description.Should().Be("Initial stock");
    [Fact] public void Constructor_SetsOrderId() => CreateOperation().OrderId.Should().Be(20);
    [Fact] public void Constructor_SetsInventoryId() => CreateOperation().InventoryId.Should().Be(1);
    [Fact] public void Constructor_LeavesInventoryUninitialized() => CreateOperation().Inventory.Should().BeNull();
    private static InventoryOperation CreateOperation() => new(true, 10, 7, 10, "Initial stock", 20, 1);
}