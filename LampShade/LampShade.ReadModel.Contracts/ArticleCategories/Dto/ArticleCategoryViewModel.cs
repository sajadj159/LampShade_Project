namespace LampShade.ReadModel.Contracts.ArticleCategories.Dto;

public class ArticleCategoryViewModel
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PictureUrl { get; set; } = string.Empty;
    public int ShowOrder { get; set; }
    public string CreationDate { get; set; } = string.Empty;
    public long ArticlesCount { get; set; }
}
