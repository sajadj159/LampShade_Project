using ShopManagement.Application.Contract.A.Product;
using MediatR;
using _0_Framework.Application;

namespace LampShade.Api.Features.Products.Commands.EditProduct;

public class EditProductCommand : IRequest<OperationResult>
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IFormFile? PictureUrl { get; set; }
    public string PictureTitle { get; set; } = string.Empty;
    public string PictureAlt { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Keywords { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public long CategoryId { get; set; }
}

public class EditProductCommandHandler : IRequestHandler<EditProductCommand, OperationResult>
{
    private readonly IProductApplication _application;

    public EditProductCommandHandler(IProductApplication application)
    {
        _application = application;
    }

    public async Task<OperationResult> Handle(EditProductCommand request, CancellationToken cancellationToken)
    {
        var command = new ShopManagement.Application.Contract.A.Product.EditProduct
        {
            Id = request.Id,
            Name = request.Name,
            Code = request.Code,
            ShortDescription = request.ShortDescription,
            Description = request.Description,
            PictureUrl = request.PictureUrl,
            PictureTitle = request.PictureTitle,
            PictureAlt = request.PictureAlt,
            Slug = request.Slug,
            Keywords = request.Keywords,
            MetaDescription = request.MetaDescription,
            CategoryId = request.CategoryId
        };
        return await Task.FromResult(_application.Edit(command));
    }
}
