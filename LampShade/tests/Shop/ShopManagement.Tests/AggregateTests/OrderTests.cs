using AwesomeAssertions;
using _0_Framework.Domain;
using ShopManagement.Domain.OrderAgg;
using Xunit;

namespace ShopManagement.Tests.AggregateTests;

public class OrderTests
{
    [Fact] public void Constructor_InheritsFromEntityBase() => CreateOrder().Should().BeAssignableTo<EntityBase>();
    [Fact] public void Constructor_SetsCreationDate() { var beforeCreation = DateTime.UtcNow; var order = CreateOrder(); order.CreationDate.Should().BeOnOrAfter(beforeCreation); order.CreationDate.Should().BeOnOrBefore(DateTime.UtcNow); }
    [Fact] public void Constructor_SetsDefaultId() => CreateOrder().Id.Should().Be(0);
    [Fact] public void Constructor_SetsAccountId() => CreateOrder().AccountId.Should().Be(1);
    [Fact] public void Constructor_SetsTotalAmount() => CreateOrder().TotalAmount.Should().Be(100_000);
    [Fact] public void Constructor_SetsPaymentMethod() => CreateOrder().PaymentMethod.Should().Be(1);
    [Fact] public void Constructor_SetsDiscountAmount() => CreateOrder().DiscountAmount.Should().Be(10_000);
    [Fact] public void Constructor_SetsPayAmount() => CreateOrder().PayAmount.Should().Be(90_000);
    [Fact] public void Constructor_SetsIsPaidToFalse() => CreateOrder().IsPaid.Should().BeFalse();
    [Fact] public void Constructor_SetsIsCanceledToFalse() => CreateOrder().IsCanceled.Should().BeFalse();
    [Fact] public void Constructor_SetsRefIdToZero() => CreateOrder().RefId.Should().Be(0);
    [Fact] public void Constructor_InitializesItems() { var order = CreateOrder(); order.Items.Should().NotBeNull(); order.Items.Should().BeEmpty(); }
    [Fact] public void Constructor_LeavesIssueTrackingNumberUninitialized() => CreateOrder().IssueTrackingNumber.Should().BeNull();
    [Fact] public void Constructor_LeavesPaymentProofUrlUninitialized() => CreateOrder().PaymentProofUrl.Should().BeNull();
    [Fact] public void PaymentSucceeded_WithReferenceId_MarksOrderAsPaidAndStoresReference() { var order = CreateOrder(); order.PaymentSucceeded(123_456); order.IsPaid.Should().BeTrue(); order.RefId.Should().Be(123_456); }
    [Fact] public void PaymentSucceeded_WithZeroReferenceId_DoesNotChangeReference() { var order = CreateOrder(); order.PaymentSucceeded(0); order.IsPaid.Should().BeTrue(); order.RefId.Should().Be(0); }
    [Fact] public void PaymentSucceeded_WithOneReferenceId_StoresReference() { var order = CreateOrder(); order.PaymentSucceeded(1); order.IsPaid.Should().BeTrue(); order.RefId.Should().Be(1); }
    [Fact] public void SetPaymentProof_SetsPaymentProofUrl() { var order = CreateOrder(); order.SetPaymentProof("proof.jpg"); order.PaymentProofUrl.Should().Be("proof.jpg"); }
    [Fact] public void Cancel_MarksOrderAsCanceled() { var order = CreateOrder(); order.Cancel(); order.IsCanceled.Should().BeTrue(); }
    [Fact] public void SetIssueTrackingNumber_SetsIssueTrackingNumber() { var order = CreateOrder(); order.SetIssueTrackingNumber("TRACK-001"); order.IssueTrackingNumber.Should().Be("TRACK-001"); }
    [Fact] public void AddItem_AddsItemToOrder() { var order = CreateOrder(); var item = new OrderItem(5, 2, 75_000, 10); order.AddItem(item); order.Items.Should().ContainSingle().Which.Should().BeSameAs(item); }
    private static Order CreateOrder() => new(1, 100_000, 1, 10_000, 90_000);
}