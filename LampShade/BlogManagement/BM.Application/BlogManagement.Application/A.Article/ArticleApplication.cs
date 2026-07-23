using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using _0_Framework.Application;
using BlogManagement.Application.Contract.AC.Article;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application.A.Article
{
    public class ArticleApplication : IArticleApplication
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IFIleUploader _uploader;
        private readonly IArticleCategoryRepository _articleCategoryRepository;

        public ArticleApplication(IArticleRepository articleRepository, IFIleUploader uploader, IArticleCategoryRepository articleCategoryRepository)
        {
            _articleRepository = articleRepository;
            _uploader = uploader;
            _articleCategoryRepository = articleCategoryRepository;
        }

        public async Task<OperationResult> CreateAsync(CreateArticle command, CancellationToken cancellationToken = default)
        {
            var operationResult = new OperationResult();
            if (await _articleRepository.ExistAsync(x => x.Title == command.Title, cancellationToken))
            {
               return operationResult.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var slugify = command.Slug.Slugify();
            var slugBy = await _articleCategoryRepository.GetSlugByAsync(command.CategoryId, cancellationToken);
            var path = $"{slugBy}/{slugify}";
            var picturePath = _uploader.Upload(command.PictureUrl, path);
            var publishDate = command.PublishDate.ToGeorgianDateTime();

            var article = new Domain.ArticleAgg.Article(command.Title, command.ShortDescription, command.Description, picturePath,
                command.PictureTitle, command.PictureAlt, publishDate, slugify, command.Keywords, command.MetaDescription, command.CanonicalAddress, command.CategoryId);
            
            _articleRepository.Add(article);
            return operationResult.Succeeded();
        }

        public async Task<OperationResult> EditAsync(EditArticle command, CancellationToken cancellationToken = default)
        {
            var operationResult = new OperationResult();
            var article = await _articleRepository.GetWithCategoryAsync(command.Id, cancellationToken);

            if (article == null)
            {
               return operationResult.Failed(ApplicationMessages.RecordNotFound);
            }

            if (await _articleRepository.ExistAsync(x => x.Title == command.Title && x.Id != command.Id, cancellationToken))
            {
               return operationResult.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var slugify = command.Slug.Slugify();
            var path = $"{article.Category.Slug}/{slugify}";
            var picturePath = _uploader.Upload(command.PictureUrl, path);
            var publishDate = command.PublishDate.ToGeorgianDateTime();

            article.Edit(command.Title, command.ShortDescription, command.Description, picturePath,
                command.PictureTitle, command.PictureAlt, publishDate, slugify, command.Keywords,
                command.MetaDescription, command.CanonicalAddress, command.CategoryId);
            return operationResult.Succeeded();
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            return _articleRepository.Search(searchModel);
        }

        public EditArticle GetDetails(long id)
        {
            return _articleRepository.GetDetails(id);
        }
    }
}
