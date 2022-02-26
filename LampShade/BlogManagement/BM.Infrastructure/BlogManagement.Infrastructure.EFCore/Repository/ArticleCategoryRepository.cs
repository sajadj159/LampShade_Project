using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Repository;
using BlogManagement.Application.Contract.AC.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Infrastructure.EFCore.Repository
{
    public class ArticleCategoryRepository : RepositoryBase<long, ArticleCategory>, IArticleCategoryRepository
    {
        private readonly BlogContext _context;
        public ArticleCategoryRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            var queryable = _context.ArticleCategories.Select(x => new ArticleCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CreationDate = x.CreationDate.ToFarsi(),
                Description = x.Description,
                PictureUrl = x.PictureUrl,
                ShowOrder = x.ShowOrder
            });
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                queryable = queryable.Where(x => x.Name.Contains(searchModel.Name));
            }

            return queryable.OrderByDescending(x => x.ShowOrder).ToList();
        }

        public EditArticleCategory GetDetails(long id)
        {
            return _context.ArticleCategories.Select(x => new EditArticleCategory
            {
                Id = x.Id,
                Description = x.Description,
                Keywords = x.Keywords,
                Slug = x.Slug,
                CanonicalAddress = x.CanonicalAddress,
                MetaDescription = x.MetaDescription,
                Name = x.Name,
                ShowOrder = x.ShowOrder,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle
            }).FirstOrDefault(x => x.Id == id);
        }
    }
}