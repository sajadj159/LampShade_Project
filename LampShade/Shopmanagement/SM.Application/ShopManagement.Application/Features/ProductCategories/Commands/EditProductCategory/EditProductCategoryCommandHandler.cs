#nullable enable

using MediatR;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contract.ProductCategory;
using _0_Framework.Application;

using ShopManagement.Application.Contracts.Commands.ProductCategories.EditProductCategory;

namespace ShopManagement.Application.Features.ProductCategories.Commands.EditProductCategory;



public class EditProductCategoryCommandHandler : IRequestHandler<EditProductCategoryCommand, OperationResult>
{
    private readonly IProductCategoryApplication _application;
    public EditProductCategoryCommandHandler(IProductCategoryApplication application) => _application = application;

    public Task<OperationResult> Handle(EditProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var command = new ShopManagement.Application.Contract.ProductCategory.EditProductCategory
        {
            Id = request.Id, Name = request.Name, Description = request.Description, PictureUrl = request.PictureUrl,
            PictureAlt = request.PictureAlt, PictureTitle = request.PictureTitle,
            Keywords = request.Keywords, MetaDescription = request.MetaDescription, Slug = request.Slug
        };
        return Task.FromResult(_application.Edit(command));
    }
}
