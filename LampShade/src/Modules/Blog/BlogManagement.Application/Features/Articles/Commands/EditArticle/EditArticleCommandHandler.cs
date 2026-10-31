using BlogManagement.Application.Contracts.Commands.Articles.EditArticle;
using BlogManagement.Domain.ArticleAgg;
using LampShade.BuildingBlocks.Application;
using MediatR;

namespace BlogManagement.Application.Features.Articles.Commands.EditArticle;

public class EditArticleCommandHandler(IArticleRepository articles, IFileUploader uploader, IHtmlSanitizer sanitizer)
    : IRequestHandler<EditArticleCommand, OperationResult>
{
    public async Task<OperationResult> Handle(EditArticleCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        var article = await articles.GetWithCategoryAsync(request.Id, cancellationToken);
        if (article is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);
        if (await articles.ExistAsync(x => x.Title == request.Title && x.Id != request.Id, cancellationToken))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        var slug = request.Slug.Slugify();
        var picturePath = uploader.Upload(request.PictureUrl, $"{article.Category.Slug}/{slug}");

        article.Edit(request.Title, request.ShortDescription, sanitizer.Sanitize(request.Description), picturePath,
            request.PictureTitle, request.PictureAlt, request.PublishDate.ToGeorgianDateTime(), slug, request.Keywords,
            request.MetaDescription, request.CanonicalAddress, request.CategoryId);
        return operation.Succeeded();
    }
}
