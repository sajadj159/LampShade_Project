using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastructure.EFCore.Mapping;
using LampShade.BuildingBlocks.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;

namespace CommentManagement.Infrastructure.EFCore
{
    public class CommentContext : DbContext, IDbContext
    {
        public const string Schema = "comment";

        public DbSet<Comment> Comments { get; set; }

        public CommentContext(DbContextOptions<CommentContext> options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CommentMapping).Assembly);
            modelBuilder.ApplyOutboxAndInbox();
            base.OnModelCreating(modelBuilder);
        }
    }
}
