using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_LampShadeQuery.Contract.Article;
using _01_LampShadeQuery.Contract.Comment;
using BlogManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace _01_LampShadeQuery.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogContext _context;
        private readonly CommentContext _commentContext;

        public ArticleQuery(BlogContext context, CommentContext commentContext)
        {
            _context = context;
            _commentContext = commentContext;
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

            var comments = _commentContext.Comments
                .Where(x => x.OwnerRecordId == result.Id)
                .Where(x => !x.IsCanceled)
                .Where(x => x.IsConfirmed)
                .Where(x => x.Type == CommentType.Article)
                .Select(x => new CommentQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ParentId = x.ParentId,
                    CreationDate = x.CreationDate.ToFarsi()
                }).OrderByDescending(x => x.Id).ToList();

            foreach (var comment in comments.Where(comment => comment.ParentId > 0))
            {
                comment.ParentName = comments.FirstOrDefault(x => x.Id == comment.ParentId)?.Name;
            }
            result.Comments= comments;
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