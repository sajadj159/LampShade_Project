using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using LampShade.ReadModel.Contracts.Article;
using LampShade.ReadModel.Contracts.ArticleCategory;
using LampShade.ReadModel.Contracts.ArticleCategories.Dto;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;
using BlogManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace LampShade.ReadModel.Application.Query
{
    public class ArticleCategoryQuery : IArticleCategoryQuery
    {
        private readonly BlogContext _context;

        public ArticleCategoryQuery(BlogContext context)
        {
            _context = context;
        }

        public ArticleCategoryQueryModel GetArticleCategory(string slug)
        {
            var queryable = _context.ArticleCategories
                .Include(x => x.Articles)
                .Select(x => new ArticleCategoryQueryModel
                {
                    Name = x.Name,
                    PictureUrl = x.PictureUrl,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Description = x.Description,
                    Slug = x.Slug,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    CanonicalAddress = x.CanonicalAddress,
                    ArticlesCount = x.Articles.Count,
                    Articles = MapArticles(x.Articles)
                }).FirstOrDefault(x => x.Slug == slug);

            if (!string.IsNullOrWhiteSpace(queryable.Keywords))
                queryable.KeywordList = queryable.Keywords.Split(",").ToList();
            return queryable;
        }

        private static List<ArticleQueryModel> MapArticles(List<Article> articles)
        {
            return articles.Select(x => new ArticleQueryModel
            {
                Id = x.Id,
                Title = x.Title,
                ShortDescription = x.ShortDescription,
                PictureUrl = x.PictureUrl,
                PictureTitle = x.PictureTitle,
                PictureAlt = x.PictureAlt,
                PublishDate = x.PublishDate.ToFarsi(),
                Slug = x.Slug
            }).ToList();
        }

        public List<ArticleCategoryQueryModel> GetArticleCategories()
        {
            return _context.ArticleCategories
                .Include(x => x.Articles)
                .Select(x => new ArticleCategoryQueryModel
                {
                    Name = x.Name,
                    PictureUrl = x.PictureUrl,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    ArticlesCount = x.Articles.Count,
                    Articles = MapArticles(x.Articles)
                }).ToList();
        }

        public ArticleCategoryDetailsDto GetArticleCategoryForManagement(long id)
        {
            return _context.ArticleCategories.AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new ArticleCategoryDetailsDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    PictureUrl = x.PictureUrl,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Description = x.Description,
                    ShowOrder = x.ShowOrder,
                    Slug = x.Slug,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    CanonicalAddress = x.CanonicalAddress
                })
                .FirstOrDefault();
        }
        public List<ArticleCategoryViewModel> GetArticleCategoriesForManagement()
        {
            return ProjectArticleCategories(_context.ArticleCategories);
        }

        public List<ArticleCategoryViewModel> SearchArticleCategories(string name)
        {
            var query = _context.ArticleCategories.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.Name.Contains(name));

            return ProjectArticleCategories(query);
        }

        private static List<ArticleCategoryViewModel> ProjectArticleCategories(IQueryable<ArticleCategory> query)
        {
            return query.AsNoTracking()
                .Select(x => new ArticleCategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    PictureUrl = x.PictureUrl,
                    ShowOrder = x.ShowOrder,
                    CreationDate = x.CreationDate.ToFarsi(),
                    ArticlesCount = x.Articles.Count
                })
                .ToList();
        }
    }
}



