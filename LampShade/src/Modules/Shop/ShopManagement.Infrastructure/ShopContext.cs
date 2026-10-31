using LampShade.BuildingBlocks.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.SlideAgg;
using ShopManagement.Infrastructure.EFCore.Mapping;

namespace ShopManagement.Infrastructure.EFCore
{
    public class ShopContext : DbContext, IDbContext
    {
        public const string Schema = "shop";

        public DbSet<Order> Orders { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<ProductPicture> ProductPictures { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }

        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductCategoryMapping).Assembly);
            modelBuilder.ApplyOutboxAndInbox();
            base.OnModelCreating(modelBuilder);
        }
    }
}
