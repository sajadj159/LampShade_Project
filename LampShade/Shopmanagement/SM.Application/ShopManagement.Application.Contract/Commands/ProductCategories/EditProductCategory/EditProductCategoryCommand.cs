#nullable enable

using MediatR;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contract.ProductCategory;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Commands.ProductCategories.EditProductCategory;

public class EditProductCategoryCommand : IRequest<OperationResult>
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IFormFile? PictureUrl { get; set; }
    public string PictureAlt { get; set; } = string.Empty;
    public string PictureTitle { get; set; } = string.Empty;
    public string Keywords { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}
