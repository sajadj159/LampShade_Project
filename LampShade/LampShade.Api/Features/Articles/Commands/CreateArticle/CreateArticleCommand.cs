using BlogManagement.Application.Contract.AC.Article;
using MediatR;
using _0_Framework.Application;

namespace LampShade.Api.Features.Articles.Commands.CreateArticle;

public class CreateArticleCommand : IRequest<OperationResult>
{
    public string Title { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IFormFile? PictureUrl { get; set; }
    public string PictureTitle { get; set; } = string.Empty;
    public string PictureAlt { get; set; } = string.Empty;
    public string PublishDate { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Keywords { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public string CanonicalAddress { get; set; } = string.Empty;
    public long CategoryId { get; set; }
}

public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, OperationResult>
{
    private readonly IArticleApplication _articleApplication;

    public CreateArticleCommandHandler(IArticleApplication articleApplication)
    {
        _articleApplication = articleApplication;
    }

    public async Task<OperationResult> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var command = new BlogManagement.Application.Contract.AC.Article.CreateArticle
        {
            Title = request.Title,
            ShortDescription = request.ShortDescription,
            Description = request.Description,
            PictureUrl = request.PictureUrl,
            PictureTitle = request.PictureTitle,
            PictureAlt = request.PictureAlt,
            PublishDate = request.PublishDate,
            Slug = request.Slug,
            Keywords = request.Keywords,
            MetaDescription = request.MetaDescription,
            CanonicalAddress = request.CanonicalAddress,
            CategoryId = request.CategoryId
        };
        return await Task.FromResult(_articleApplication.Create(command));
    }
}
