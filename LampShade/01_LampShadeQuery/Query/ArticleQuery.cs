using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_LampShadeQuery.Contract.Article;
using BlogManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace _01_LampShadeQuery.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogContext _context;

        public ArticleQuery(BlogContext context)
        {
            _context = context;
        }

        public ArticleQueryModel GetArticleDetails(string value)
        {
            var result = _context.Articles
                .Where(x => x.PublishDate <= DateTime.Now)
                .Include(x => x.Category)
                .Select(x => new ArticleQueryModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    ShortDescription = x.ShortDescription,
                    Description = x.Description,
                    PictureUrl = x.PictureUrl,
                    PictureTitle = x.PictureTitle,
                    PictureAlt = x.PictureAlt,
                    PublishDate = x.PublishDate.ToFarsi(),
                    Slug = x.Slug,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    CanonicalAddress = x.CanonicalAddress,
                    CategoryName = x.Category.Name,
                    CategorySlug = x.Category.Slug
                }).FirstOrDefault(x => x.Slug == value);
            if (!string.IsNullOrWhiteSpace(result.Keywords))
                result.KeywordsList = result.Keywords.Split(",").ToList();
            return result;
        }

        public List<ArticleQueryModel> LatestArticles()
        {
            return _context.Articles
                .Where(x => x.PublishDate <= DateTime.Now)
                .Include(x => x.Category)
                .Select(x => new ArticleQueryModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    ShortDescription = x.ShortDescription,
                    PictureUrl = x.PictureUrl,
                    PictureTitle = x.PictureTitle,
                    PictureAlt = x.PictureAlt,
                    PublishDate = x.PublishDate.ToFarsi(),
                    Slug = x.Slug,
                }).ToList();
        }
    }
}