using AwesomeAssertions;
using _0_Framework.Domain;
using DiscountManagement.Domain.CustomerDiscountAgg;
using Xunit;

namespace DiscountManagement.Tests.AggregateTests;

public class CustomerDiscountTests
{
    [Fact] public void DefaultConstructor_InheritsFromEntityBase() => new CustomerDiscount().Should().BeAssignableTo<EntityBase>();
    [Fact] public void DefaultConstructor_SetsCreationDate() { var beforeCreation = DateTime.UtcNow; var discount = new CustomerDiscount(); discount.CreationDate.Should().BeOnOrAfter(beforeCreation); discount.CreationDate.Should().BeOnOrBefore(DateTime.UtcNow); }
    [Fact] public void DefaultConstructor_SetsDefaultId() => new CustomerDiscount().Id.Should().Be(0);
    [Fact] public void ParameterizedConstructor_SetsProductId() => CreateDiscount().ProductId.Should().Be(1);
    [Fact] public void ParameterizedConstructor_SetsDiscountRate() => CreateDiscount().DiscountRate.Should().Be(20);
    [Fact] public void ParameterizedConstructor_SetsStartDate() => CreateDiscount().StartDate.Should().Be(StartDate);
    [Fact] public void ParameterizedConstructor_SetsEndDate() => CreateDiscount().EndDate.Should().Be(EndDate);
    [Fact] public void ParameterizedConstructor_SetsReason() => CreateDiscount().Reason.Should().Be("Seasonal promotion");
    [Fact] public void Edit_UpdatesProductId() { var discount = CreateDiscount(); discount.Edit(2, 25, UpdatedStartDate, UpdatedEndDate, "Updated promotion"); discount.ProductId.Should().Be(2); }
    [Fact] public void Edit_UpdatesDiscountRate() { var discount = CreateDiscount(); discount.Edit(2, 25, UpdatedStartDate, UpdatedEndDate, "Updated promotion"); discount.DiscountRate.Should().Be(25); }
    [Fact] public void Edit_UpdatesStartDate() { var discount = CreateDiscount(); discount.Edit(2, 25, UpdatedStartDate, UpdatedEndDate, "Updated promotion"); discount.StartDate.Should().Be(UpdatedStartDate); }
    [Fact] public void Edit_UpdatesEndDate() { var discount = CreateDiscount(); discount.Edit(2, 25, UpdatedStartDate, UpdatedEndDate, "Updated promotion"); discount.EndDate.Should().Be(UpdatedEndDate); }
    [Fact] public void Edit_UpdatesReason() { var discount = CreateDiscount(); discount.Edit(2, 25, UpdatedStartDate, UpdatedEndDate, "Updated promotion"); discount.Reason.Should().Be("Updated promotion"); }
    private static readonly DateTime StartDate = new(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    private static readonly DateTime EndDate = new(2026, 1, 31, 0, 0, 0, DateTimeKind.Utc);
    private static readonly DateTime UpdatedStartDate = new(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc);
    private static readonly DateTime UpdatedEndDate = new(2026, 2, 28, 0, 0, 0, DateTimeKind.Utc);
    private static CustomerDiscount CreateDiscount() => new(1, 20, StartDate, EndDate, "Seasonal promotion");
}