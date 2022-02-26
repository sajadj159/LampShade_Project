using System.Collections.Generic;
using _0_Framework.Domain;
using BlogManagement.Application.Contract.AC.ArticleCategory;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public interface IArticleCategoryRepository:IRepository<long ,ArticleCategory>
    {
        List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);
        EditArticleCategory GetDetails(long id);
    }
}