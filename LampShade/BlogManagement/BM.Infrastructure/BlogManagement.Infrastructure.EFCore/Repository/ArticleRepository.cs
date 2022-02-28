using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Repository;
using BlogManagement.Application.Contract.AC.Article;
using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.EFCore.Repository
{
    public class ArticleRepository : RepositoryBase<long, Article>, IArticleRepository
    {
        private readonly BlogContext _context;
        public ArticleRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        public Article GetWithCategory(long id)
        {
            return _context.Articles.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            var queryable = _context.Articles.Select(x => new ArticleViewModel
            {
                Id = x.Id,
                CreatedDate = x.CreationDate.ToFarsi(),
                PictureUrl = x.PictureUrl,
                Title = x.Title,
                ShortDescription = x.ShortDescription.Substring(0, Math.Min(x.ShortDescription.Length, 50)) + "...",
                PublishDate = x.PublishDate.ToFarsi(),
                Category = x.Category.Name,
                CategoryId = x.CategoryId
            });
            if (searchModel.CategoryId > 0)
            {
                queryable = queryable.Where(x => x.CategoryId == searchModel.CategoryId);
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Title))
            {
                queryable = queryable.Where(x => x.Title.Contains(searchModel.Title));
            }

            return queryable.OrderByDescending(x => x.Id).ToList();
        }

        public EditArticle GetDetails(long id)
        {
            return _context.Articles.Select(x => new EditArticle
            {
                Title = x.Title,
                ShortDescription = x.ShortDescription,
                Description = x.Description,
                PictureTitle = x.PictureTitle,
                PictureAlt = x.PictureAlt,
                PublishDate = x.PublishDate.ToFarsi(),
                Slug = x.Slug,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                CanonicalAddress = x.CanonicalAddress,
                CategoryId = x.CategoryId,
                Id = x.Id
            }).FirstOrDefault(x => x.Id == id);
        }
    }
}