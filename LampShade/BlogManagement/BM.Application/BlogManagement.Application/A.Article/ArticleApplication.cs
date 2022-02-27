using System.Collections.Generic;
using _0_Framework.Application;
using BlogManagement.Application.Contract.AC.Article;
using BlogManagement.Domain.ArticleAgg;

namespace BlogManagement.Application.A.Article
{
    public class ArticleApplication : IArticleApplication
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IFIleUploader _uploader;

        public ArticleApplication(IArticleRepository articleRepository, IFIleUploader uploader)
        {
            _articleRepository = articleRepository;
            _uploader = uploader;
        }

        public OperationResult Create(CreateArticle command)
        {
            var operationResult = new OperationResult();
            if (_articleRepository.Exist(x=>x.Title==command.Title))
            {
                operationResult.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var slugify = command.Slug.Slugify();
            var picturePath = _uploader.Upload(command.PictureUrl,slugify);
            var publishDate = command.PublishDate.FromFarsiDate().Value;
            var article = new Domain.ArticleAgg.Article(command.Title, command.ShortDescription, command.Description, picturePath,
                command.PictureTitle, command.PictureAlt, publishDate, command.Slug,command.Keywords,command.MetaDescription,command.CanonicalAddress,command.CategoryId);
            _articleRepository.Create(article);
            _articleRepository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditArticle command)
        {
            throw new System.NotImplementedException();
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            throw new System.NotImplementedException();
        }

        public EditArticle GetDetails(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}