using AccountManagement.Domain.AccountAgg;
using AccountManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AccountManagement.Infrastructure.Seed;

public static class AccountSeedData
{
    public const string BootstrapAdminSection = "Account:BootstrapAdmin";

    public static async Task SeedDefaultRolesAsync(this AccountContext context, CancellationToken cancellationToken)
    {
        var createdAt = DateTime.UtcNow;
        var schema = AccountContext.Schema;

        await context.Database.ExecuteSqlRawAsync(
            $$"""
            INSERT INTO {{schema}}."Roles" ("Id", "Name", "CreationDate")
            VALUES (1, 'مدیر سیستم', {0}), (2, 'کاربر سیستم', {0})
            ON CONFLICT ("Id") DO UPDATE SET "Name" = EXCLUDED."Name";
            """,
            [createdAt], cancellationToken);

        await context.Database.ExecuteSqlRawAsync(
            $"""
            INSERT INTO {schema}."RolePermissions" ("Code", "RoleId")
            SELECT seed."Code", 1
            FROM (VALUES
                (10), (11), (12), (13),
                (20), (21), (22), (23), (24),
                (50), (51), (52), (53), (54), (55), (56)
            ) AS seed("Code")
            WHERE NOT EXISTS (
                SELECT 1
                FROM {schema}."RolePermissions" existing
                WHERE existing."RoleId" = 1 AND existing."Code" = seed."Code"
            );
            """, cancellationToken);

        await context.Database.ExecuteSqlRawAsync(
            $"""
            SELECT setval(
                pg_get_serial_sequence('{schema}."Roles"', 'Id'),
                GREATEST((SELECT COALESCE(MAX("Id"), 1) FROM {schema}."Roles"), 1)
            );
            """, cancellationToken);
    }

    /// <summary>
    /// Self-registration can no longer create administrators, so the first one comes from configuration
    /// (<c>Account:BootstrapAdmin:UserName/Password/Mobile/FullName</c>). Nothing happens when an
    /// administrator already exists or the section is not configured.
    /// </summary>
    public static async Task SeedBootstrapAdminAsync(this AccountContext context, IConfiguration configuration,
        LampShade.BuildingBlocks.Application.IPasswordHasher passwordHasher, CancellationToken cancellationToken)
    {
        var section = configuration.GetSection(BootstrapAdminSection);
        var userName = section["UserName"];
        var password = section["Password"];
        if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            return;

        var administratorRoleId = long.Parse(LampShade.BuildingBlocks.Application.Roles.Administrator);
        if (await context.Accounts.AnyAsync(x => x.RoleId == administratorRoleId || x.UserName == userName, cancellationToken))
            return;

        context.Accounts.Add(new Account(userName, section["FullName"] ?? "Administrator", passwordHasher.Hash(password),
            section["Mobile"] ?? string.Empty, administratorRoleId, string.Empty, string.Empty, string.Empty));
        await context.SaveChangesAsync(cancellationToken);
    }
}
