using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.EFCore.Mapping;
using LampShade.BuildingBlocks.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.EFCore
{
    public class InventoryContext : DbContext, IDbContext
    {
        public const string Schema = "inventory";

        public DbSet<Inventory> Inventory { get; set; }

        public InventoryContext(DbContextOptions<InventoryContext> options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryMapping).Assembly);
            modelBuilder.ApplyOutboxAndInbox();
            base.OnModelCreating(modelBuilder);
        }
    }
}
