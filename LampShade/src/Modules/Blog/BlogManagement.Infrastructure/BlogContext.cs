using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;
using BlogManagement.Infrastructure.EFCore.Mapping;
using LampShade.BuildingBlocks.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.EFCore
{
    public class BlogContext : DbContext, IDbContext
    {
        public const string Schema = "blog";

        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArticleCategoryMapping).Assembly);
            modelBuilder.ApplyOutboxAndInbox();
            base.OnModelCreating(modelBuilder);
        }
    }
}
