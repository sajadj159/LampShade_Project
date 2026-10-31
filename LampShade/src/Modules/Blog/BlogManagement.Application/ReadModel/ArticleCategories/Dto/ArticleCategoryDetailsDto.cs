namespace LampShade.ReadModel.Contracts.ArticleCategories.Dto;

public class ArticleCategoryDetailsDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PictureUrl { get; set; } = string.Empty;
    public string PictureAlt { get; set; } = string.Empty;
    public string PictureTitle { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ShowOrder { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Keywords { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public string CanonicalAddress { get; set; } = string.Empty;
}
