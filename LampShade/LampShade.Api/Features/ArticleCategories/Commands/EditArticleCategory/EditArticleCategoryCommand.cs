using BlogManagement.Application.Contract.AC.ArticleCategory;
using MediatR;
using _0_Framework.Application;

namespace LampShade.Api.Features.ArticleCategories.Commands.EditArticleCategory;

public class EditArticleCategoryCommand : IRequest<OperationResult>
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public IFormFile? PictureUrl { get; set; }
    public string PictureAlt { get; set; } = string.Empty;
    public string PictureTitle { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ShowOrder { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Keywords { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public string CanonicalAddress { get; set; } = string.Empty;
}

public class EditArticleCategoryCommandHandler : IRequestHandler<EditArticleCategoryCommand, OperationResult>
{
    private readonly IArticleCategoryApplication _application;
    public EditArticleCategoryCommandHandler(IArticleCategoryApplication application) => _application = application;

    public async Task<OperationResult> Handle(EditArticleCategoryCommand request, CancellationToken cancellationToken)
    {
        var command = new BlogManagement.Application.Contract.AC.ArticleCategory.EditArticleCategory
        {
            Id = request.Id, Name = request.Name, PictureUrl = request.PictureUrl, PictureAlt = request.PictureAlt,
            PictureTitle = request.PictureTitle, Description = request.Description,
            ShowOrder = request.ShowOrder, Slug = request.Slug, Keywords = request.Keywords,
            MetaDescription = request.MetaDescription, CanonicalAddress = request.CanonicalAddress
        };
        return await Task.FromResult(_application.Edit(command));
    }
}
