#nullable enable

using ShopManagement.Application.Contract.A.Product;
using MediatR;
using Microsoft.AspNetCore.Http;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Commands.Products.EditProduct;

public class EditProductCommand : IRequest<OperationResult>
{
    public long Id { get; set; }
    public bool ClearMainPicture { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IFormFile? PictureUrl { get; set; }
    public List<IFormFile> AdditionalPictures { get; set; } = new();
    public string PictureTitle { get; set; } = string.Empty;
    public string PictureAlt { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Keywords { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public long CategoryId { get; set; }
}
