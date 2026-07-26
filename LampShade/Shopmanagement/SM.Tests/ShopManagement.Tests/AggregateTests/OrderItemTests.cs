using AwesomeAssertions;
using _0_Framework.Domain;
using ShopManagement.Domain.OrderAgg;
using Xunit;

namespace ShopManagement.Tests.AggregateTests;

public class OrderItemTests
{
    [Fact] public void Constructor_InheritsFromEntityBase() => CreateItem().Should().BeAssignableTo<EntityBase>();
    [Fact] public void Constructor_SetsCreationDate() { var beforeCreation = DateTime.UtcNow; var item = CreateItem(); item.CreationDate.Should().BeOnOrAfter(beforeCreation); item.CreationDate.Should().BeOnOrBefore(DateTime.UtcNow); }
    [Fact] public void Constructor_SetsDefaultId() => CreateItem().Id.Should().Be(0);
    [Fact] public void Constructor_SetsProductId() => CreateItem().ProductId.Should().Be(5);
    [Fact] public void Constructor_SetsCount() => CreateItem().Count.Should().Be(2);
    [Fact] public void Constructor_SetsUnitPrice() => CreateItem().UnitPrice.Should().Be(75_000);
    [Fact] public void Constructor_SetsDiscountRate() => CreateItem().DiscountRate.Should().Be(10);
    [Fact] public void Constructor_SetsOrderIdToZero() => CreateItem().OrderId.Should().Be(0);
    [Fact] public void Constructor_LeavesOrderUninitialized() => CreateItem().Order.Should().BeNull();
    private static OrderItem CreateItem() => new(5, 2, 75_000, 10);
}