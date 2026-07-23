#nullable enable

using BlogManagement.Application.Contract.AC.ArticleCategory;
using MediatR;
using Microsoft.AspNetCore.Http;
using _0_Framework.Application;

namespace BlogManagement.Application.Contracts.Commands.ArticleCategories.CreateArticleCategory;

public class CreateArticleCategoryCommand : ICommand<OperationResult>
{
    public string Name { get; set; } = string.Empty;
    public IFormFile? PictureUrl { get; set; }
    public string PictureAlt { get; set; } = string.Empty;
    public string PictureTitle { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ShowOrder { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Keywords { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public string CanonicalAddress { get; set; } = string.Empty;
}
