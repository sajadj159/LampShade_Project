using AwesomeAssertions;
using _0_Framework.Domain;
using ShopManagement.Domain.SlideAgg;
using Xunit;

namespace ShopManagement.Tests.AggregateTests;

public class SlideTests
{
    [Fact] public void DefaultConstructor_InheritsFromEntityBase() => new Slide().Should().BeAssignableTo<EntityBase>();
    [Fact] public void DefaultConstructor_SetsCreationDate() { var beforeCreation = DateTime.UtcNow; var slide = new Slide(); slide.CreationDate.Should().BeOnOrAfter(beforeCreation); slide.CreationDate.Should().BeOnOrBefore(DateTime.UtcNow); }
    [Fact] public void DefaultConstructor_SetsDefaultId() => new Slide().Id.Should().Be(0);
    [Fact] public void ParameterizedConstructor_SetsPictureUrl() => CreateSlide().PictureUrl.Should().Be("slide.jpg");
    [Fact] public void ParameterizedConstructor_SetsPictureAlt() => CreateSlide().PictureAlt.Should().Be("Slide alt");
    [Fact] public void ParameterizedConstructor_SetsPictureTitle() => CreateSlide().PictureTitle.Should().Be("Slide title");
    [Fact] public void ParameterizedConstructor_SetsHeading() => CreateSlide().Heading.Should().Be("Heading");
    [Fact] public void ParameterizedConstructor_SetsTitle() => CreateSlide().Title.Should().Be("Title");
    [Fact] public void ParameterizedConstructor_SetsText() => CreateSlide().Text.Should().Be("Text");
    [Fact] public void ParameterizedConstructor_SetsBtnText() => CreateSlide().BtnText.Should().Be("Shop now");
    [Fact] public void ParameterizedConstructor_SetsLink() => CreateSlide().Link.Should().Be("/products");
    [Fact] public void ParameterizedConstructor_SetsIsRemovedToFalse() => CreateSlide().IsRemoved.Should().BeFalse();
    [Fact]
    public void Edit_WithPictureUrl_UpdatesAllEditableProperties()
    {
        var slide = CreateSlide(); slide.Edit("updated.jpg", "Updated alt", "Updated title", "Updated heading", "Updated title text", "Updated text", "View products", "/updated-products");
        slide.PictureUrl.Should().Be("updated.jpg"); slide.PictureAlt.Should().Be("Updated alt"); slide.PictureTitle.Should().Be("Updated title"); slide.Heading.Should().Be("Updated heading"); slide.Title.Should().Be("Updated title text"); slide.Text.Should().Be("Updated text"); slide.BtnText.Should().Be("View products"); slide.Link.Should().Be("/updated-products");
    }
    [Fact] public void Edit_WithWhitespacePictureUrl_PreservesCurrentPicture() { var slide = CreateSlide(); slide.Edit(" ", "Updated alt", "Updated title", "Updated heading", "Updated title text", "Updated text", "View products", "/updated-products"); slide.PictureUrl.Should().Be("slide.jpg"); }
    [Fact] public void Remove_MarksSlideAsRemoved() { var slide = CreateSlide(); slide.Remove(); slide.IsRemoved.Should().BeTrue(); }
    [Fact] public void Restore_AfterRemove_MarksSlideAsActive() { var slide = CreateSlide(); slide.Remove(); slide.Restore(); slide.IsRemoved.Should().BeFalse(); }
    private static Slide CreateSlide() => new("slide.jpg", "Slide alt", "Slide title", "Heading", "Title", "Text", "Shop now", "/products");
}