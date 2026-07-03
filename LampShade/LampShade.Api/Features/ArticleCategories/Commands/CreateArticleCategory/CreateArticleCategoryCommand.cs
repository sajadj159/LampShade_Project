using BlogManagement.Application.Contract.AC.ArticleCategory;
using MediatR;
using _0_Framework.Application;

namespace LampShade.Api.Features.ArticleCategories.Commands.CreateArticleCategory;

public class CreateArticleCategoryCommand : IRequest<OperationResult>
{
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

public class CreateArticleCategoryCommandHandler : IRequestHandler<CreateArticleCategoryCommand, OperationResult>
{
    private readonly IArticleCategoryApplication _application;
    public CreateArticleCategoryCommandHandler(IArticleCategoryApplication application) => _application = application;

    public async Task<OperationResult> Handle(CreateArticleCategoryCommand request, CancellationToken cancellationToken)
    {
        var command = new BlogManagement.Application.Contract.AC.ArticleCategory.CreateArticleCategory
        {
            Name = request.Name, PictureUrl = request.PictureUrl, PictureAlt = request.PictureAlt,
            PictureTitle = request.PictureTitle, Description = request.Description,
            ShowOrder = request.ShowOrder, Slug = request.Slug, Keywords = request.Keywords,
            MetaDescription = request.MetaDescription, CanonicalAddress = request.CanonicalAddress
        };
        return await Task.FromResult(_application.Create(command));
    }
}
