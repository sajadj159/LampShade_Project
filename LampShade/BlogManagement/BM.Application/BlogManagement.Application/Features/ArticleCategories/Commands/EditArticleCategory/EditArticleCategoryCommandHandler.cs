using _0_Framework.Application;
using BlogManagement.Application.Contracts.Commands.ArticleCategories.EditArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using MediatR;

namespace BlogManagement.Application.Features.ArticleCategories.Commands.EditArticleCategory;

public class EditArticleCategoryCommandHandler(
    IArticleCategoryRepository articleCategoryRepository,
    IFIleUploader uploader) : IRequestHandler<EditArticleCategoryCommand, OperationResult>
{
    public Task<OperationResult> Handle(EditArticleCategoryCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        var category = articleCategoryRepository.Get(request.Id);
        if (category is null)
            return Task.FromResult(operation.Failed(ApplicationMessages.RecordNotFound));
        if (articleCategoryRepository.Exist(x => x.Name == request.Name && x.Id != request.Id))
            return Task.FromResult(operation.Failed(ApplicationMessages.DuplicatedRecord));

        var slug = request.Slug.Slugify();
        var picturePath = uploader.Upload(request.PictureUrl, slug);
        category.Edit(request.Name, picturePath, request.PictureAlt, request.PictureTitle,
            request.Description, request.ShowOrder, slug, request.Keywords, request.MetaDescription,
            request.CanonicalAddress);
        articleCategoryRepository.Save();
        return Task.FromResult(operation.Succeeded());
    }
}
