namespace LampShade.ReadModel.Contracts.Articles.Dto;

public class ArticleViewModel
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string PictureUrl { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string PublishDate { get; set; } = string.Empty;
    public string CreatedDate { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public long CategoryId { get; set; }
}

public class ArticleDetailsDto
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PictureUrl { get; set; } = string.Empty;
    public string PictureTitle { get; set; } = string.Empty;
    public string PictureAlt { get; set; } = string.Empty;
    public string PublishDate { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Keywords { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public string CanonicalAddress { get; set; } = string.Empty;
    public long CategoryId { get; set; }
}
