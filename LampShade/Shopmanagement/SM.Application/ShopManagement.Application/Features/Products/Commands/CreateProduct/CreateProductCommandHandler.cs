using System.Threading;
using System.Threading.Tasks;
#nullable enable

using ShopManagement.Application.Contract.A.Product;
using MediatR;
using Microsoft.AspNetCore.Http;
using _0_Framework.Application;

using ShopManagement.Application.Contracts.Commands.Products.CreateProduct;

namespace ShopManagement.Application.Features.Products.Commands.CreateProduct;



public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, OperationResult>
{
    private readonly IProductApplication _application;

    public CreateProductCommandHandler(IProductApplication application)
    {
        _application = application;
    }

    public async Task<OperationResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var command = new ShopManagement.Application.Contract.A.Product.CreateProduct
        {
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
        return await _application.CreateAsync(command, cancellationToken);
    }
}
