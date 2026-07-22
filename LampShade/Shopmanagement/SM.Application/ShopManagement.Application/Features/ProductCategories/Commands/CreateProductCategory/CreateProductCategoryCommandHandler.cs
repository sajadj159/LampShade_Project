#nullable enable

using MediatR;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contract.ProductCategory;
using _0_Framework.Application;

using ShopManagement.Application.Contracts.Commands.ProductCategories.CreateProductCategory;

namespace ShopManagement.Application.Features.ProductCategories.Commands.CreateProductCategory;



public class CreateProductCategoryCommandHandler : IRequestHandler<CreateProductCategoryCommand, OperationResult>
{
    private readonly IProductCategoryApplication _application;
    public CreateProductCategoryCommandHandler(IProductCategoryApplication application) => _application = application;

    public Task<OperationResult> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var command = new ShopManagement.Application.Contract.ProductCategory.CreateProductCategory
        {
            Name = request.Name, Description = request.Description, PictureUrl = request.PictureUrl,
            PictureAlt = request.PictureAlt, PictureTitle = request.PictureTitle,
            Keywords = request.Keywords, MetaDescription = request.MetaDescription, Slug = request.Slug
        };
        return Task.FromResult(_application.Create(command));
    }
}
