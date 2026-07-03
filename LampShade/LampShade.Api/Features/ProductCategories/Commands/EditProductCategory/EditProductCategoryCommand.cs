using MediatR;
using ShopManagement.Application.Contract.ProductCategory;
using _0_Framework.Application;

namespace LampShade.Api.Features.ProductCategories.Commands.EditProductCategory;

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

public class EditProductCategoryCommandHandler : IRequestHandler<EditProductCategoryCommand, OperationResult>
{
    private readonly IProductCategoryApplication _application;
    public EditProductCategoryCommandHandler(IProductCategoryApplication application) => _application = application;

    public async Task<OperationResult> Handle(EditProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var command = new ShopManagement.Application.Contract.ProductCategory.EditProductCategory
        {
            Id = request.Id, Name = request.Name, Description = request.Description, PictureUrl = request.PictureUrl,
            PictureAlt = request.PictureAlt, PictureTitle = request.PictureTitle,
            Keywords = request.Keywords, MetaDescription = request.MetaDescription, Slug = request.Slug
        };
        return await Task.FromResult(_application.Edit(command));
    }
}
