using AwesomeAssertions;
using _0_Framework.Domain;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using Xunit;

namespace DiscountManagement.Tests.AggregateTests;

public class ColleagueDiscountTests
{
    [Fact] public void DefaultConstructor_InheritsFromEntityBase() => new ColleagueDiscount().Should().BeAssignableTo<EntityBase>();
    [Fact] public void DefaultConstructor_SetsCreationDate() { var beforeCreation = DateTime.UtcNow; var discount = new ColleagueDiscount(); discount.CreationDate.Should().BeOnOrAfter(beforeCreation); discount.CreationDate.Should().BeOnOrBefore(DateTime.UtcNow); }
    [Fact] public void DefaultConstructor_SetsDefaultId() => new ColleagueDiscount().Id.Should().Be(0);
    [Fact] public void ParameterizedConstructor_SetsProductId() => CreateDiscount().ProductId.Should().Be(1);
    [Fact] public void ParameterizedConstructor_SetsDiscountRate() => CreateDiscount().DiscountRate.Should().Be(15);
    [Fact] public void ParameterizedConstructor_SetsIsRemovedToFalse() => CreateDiscount().IsRemoved.Should().BeFalse();
    [Fact] public void Edit_UpdatesProductId() { var discount = CreateDiscount(); discount.Edit(2, 20); discount.ProductId.Should().Be(2); }
    [Fact] public void Edit_UpdatesDiscountRate() { var discount = CreateDiscount(); discount.Edit(2, 20); discount.DiscountRate.Should().Be(20); }
    [Fact] public void Remove_MarksDiscountAsRemoved() { var discount = CreateDiscount(); discount.Remove(); discount.IsRemoved.Should().BeTrue(); }
    [Fact] public void Restore_AfterRemove_MarksDiscountAsActive() { var discount = CreateDiscount(); discount.Remove(); discount.Restore(); discount.IsRemoved.Should().BeFalse(); }
    private static ColleagueDiscount CreateDiscount() => new(1, 15);
}