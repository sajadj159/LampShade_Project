using DiscountManagement.Domain.ColleagueDiscountAgg;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Infrastructure.EFCore.Mapping;
using LampShade.BuildingBlocks.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;

namespace DiscountManagement.Infrastructure.EFCore
{
    public class DiscountContext : DbContext, IDbContext
    {
        public const string Schema = "discount";

        public DbSet<CustomerDiscount> CustomerDiscounts { get; set; }
        public DbSet<ColleagueDiscount> ColleagueDiscounts { get; set; }

        public DiscountContext(DbContextOptions<DiscountContext> options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerDiscountMapping).Assembly);
            modelBuilder.ApplyOutboxAndInbox();
            base.OnModelCreating(modelBuilder);
        }
    }
}
