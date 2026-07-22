#nullable enable

using MediatR;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contract.ProductCategory;
using _0_Framework.Application;

namespace ShopManagement.Application.Features.ProductCategories.Commands.CreateProductCategory;

public class CreateProductCategoryCommand : IRequest<OperationResult>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IFormFile? PictureUrl { get; set; }
    public string PictureAlt { get; set; } = string.Empty;
    public string PictureTitle { get; set; } = string.Empty;
    public string Keywords { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}

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
