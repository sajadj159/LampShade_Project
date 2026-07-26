using AwesomeAssertions;
using _0_Framework.Domain;
using CommentManagement.Domain.CommentAgg;
using Xunit;

namespace CommentManagement.Tests.AggregateTests;

public class CommentTests
{
    [Fact] public void Constructor_InheritsFromEntityBase() => CreateComment().Should().BeAssignableTo<EntityBase>();
    [Fact] public void Constructor_SetsCreationDate() { var beforeCreation = DateTime.UtcNow; var comment = CreateComment(); comment.CreationDate.Should().BeOnOrAfter(beforeCreation); comment.CreationDate.Should().BeOnOrBefore(DateTime.UtcNow); }
    [Fact] public void Constructor_SetsDefaultId() => CreateComment().Id.Should().Be(0);
    [Fact] public void Constructor_SetsName() => CreateComment().Name.Should().Be("Jane Doe");
    [Fact] public void Constructor_SetsEmail() => CreateComment().Email.Should().Be("jane@example.com");
    [Fact] public void Constructor_SetsWebsite() => CreateComment().Website.Should().Be("https://example.com");
    [Fact] public void Constructor_SetsDescription() => CreateComment().Description.Should().Be("Helpful product review.");
    [Fact] public void Constructor_SetsRating() => CreateComment().Rating.Should().Be(5);
    [Fact] public void Constructor_SetsOwnerRecordId() => CreateComment().OwnerRecordId.Should().Be(10);
    [Fact] public void Constructor_SetsType() => CreateComment().Type.Should().Be(1);
    [Fact] public void Constructor_SetsParentId() => CreateComment().ParentId.Should().Be(2);
    [Fact] public void Constructor_SetsIsConfirmedToFalse() => CreateComment().IsConfirmed.Should().BeFalse();
    [Fact] public void Constructor_SetsIsCanceledToFalse() => CreateComment().IsCanceled.Should().BeFalse();
    [Fact] public void Constructor_LeavesParentUninitialized() => CreateComment().Parent.Should().BeNull();
    [Fact] public void Confirm_MarksCommentAsConfirmed() { var comment = CreateComment(); comment.Confirm(); comment.IsConfirmed.Should().BeTrue(); }
    [Fact] public void Cancel_MarksCommentAsCanceled() { var comment = CreateComment(); comment.Cancel(); comment.IsCanceled.Should().BeTrue(); }
    private static Comment CreateComment() => new("Jane Doe", "jane@example.com", "https://example.com", "Helpful product review.", 5, 10, 1, 2);
}