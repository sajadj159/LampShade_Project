using _0_Framework.Application;
using BlogManagement.Application.Contracts.Commands.ArticleCategories.CreateArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using MediatR;

namespace BlogManagement.Application.Features.ArticleCategories.Commands.CreateArticleCategory;

public class CreateArticleCategoryCommandHandler(
    IArticleCategoryRepository articleCategoryRepository,
    IFIleUploader uploader) : IRequestHandler<CreateArticleCategoryCommand, OperationResult>
{
    public Task<OperationResult> Handle(CreateArticleCategoryCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        if (articleCategoryRepository.Exist(x => x.Name == request.Name))
            return Task.FromResult(operation.Failed(ApplicationMessages.DuplicatedRecord));

        var slug = request.Slug.Slugify();
        var picturePath = uploader.Upload(request.PictureUrl, slug);
        var category = new ArticleCategory(request.Name, picturePath, request.PictureAlt, request.PictureTitle,
            request.Description, request.ShowOrder, slug, request.Keywords, request.MetaDescription,
            request.CanonicalAddress);

        articleCategoryRepository.Create(category);
        articleCategoryRepository.Save();
        return Task.FromResult(operation.Succeeded());
    }
}
