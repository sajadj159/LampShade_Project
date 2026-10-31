using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Infrastructure.EFCore.Mapping;
using LampShade.BuildingBlocks.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore
{
    public class AccountContext : DbContext, IDbContext
    {
        public const string Schema = "account";

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }

        public AccountContext(DbContextOptions<AccountContext> options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountMapping).Assembly);
            modelBuilder.ApplyOutboxAndInbox();
            base.OnModelCreating(modelBuilder);
        }
    }
}
