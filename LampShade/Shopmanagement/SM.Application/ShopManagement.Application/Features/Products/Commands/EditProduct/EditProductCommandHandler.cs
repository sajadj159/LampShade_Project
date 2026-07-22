#nullable enable

using ShopManagement.Application.Contract.A.Product;
using MediatR;
using Microsoft.AspNetCore.Http;
using _0_Framework.Application;

using ShopManagement.Application.Contracts.Commands.Products.EditProduct;

namespace ShopManagement.Application.Features.Products.Commands.EditProduct;



public class EditProductCommandHandler : IRequestHandler<EditProductCommand, OperationResult>
{
    private readonly IProductApplication _application;

    public EditProductCommandHandler(IProductApplication application)
    {
        _application = application;
    }

    public Task<OperationResult> Handle(EditProductCommand request, CancellationToken cancellationToken)
    {
        var command = new ShopManagement.Application.Contract.A.Product.EditProduct
        {
            Id = request.Id,
            ClearMainPicture = request.ClearMainPicture,
            Name = request.Name,
            Code = request.Code,
            ShortDescription = request.ShortDescription,
            Description = request.Description,
            PictureUrl = request.PictureUrl,
            AdditionalPictures = request.AdditionalPictures,
            PictureTitle = request.PictureTitle,
            PictureAlt = request.PictureAlt,
            Slug = request.Slug,
            Keywords = request.Keywords,
            MetaDescription = request.MetaDescription,
            CategoryId = request.CategoryId
        };
        return Task.FromResult(_application.Edit(command));
    }
}
