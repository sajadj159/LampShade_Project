#nullable enable

using ShopManagement.Application.Contract.A.ProductPicture;
using MediatR;
using Microsoft.AspNetCore.Http;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Commands.ProductPictures.CreateProductPicture;

public class CreateProductPictureCommand : ICommand<OperationResult>
{
    public long ProductId { get; set; }
    public IFormFile? PictureUrl { get; set; }
    public string PictureAlt { get; set; } = string.Empty;
    public string PictureTitle { get; set; } = string.Empty;
}
