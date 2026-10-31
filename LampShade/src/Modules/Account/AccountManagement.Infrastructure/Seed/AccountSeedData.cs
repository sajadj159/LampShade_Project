using AccountManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace LampShade.Api.SeedData;

public static class AccountSeedData
{
    public static void SeedDefaultRoles(this AccountContext context)
    {
        var createdAt = DateTime.UtcNow;

        context.Database.ExecuteSqlRaw(
            """
            INSERT INTO "Roles" ("Id", "Name", "CreationDate")
            VALUES (1, 'مدیر سیستم', {0}), (2, 'کاربر سیستم', {0})
            ON CONFLICT ("Id") DO UPDATE SET "Name" = EXCLUDED."Name";
            """,
            createdAt);

        context.Database.ExecuteSqlRaw(
            """
            INSERT INTO "RolePermissions" ("Code", "RoleId")
            SELECT seed."Code", 1
            FROM (VALUES
                (10), (11), (12), (13),
                (20), (21), (22), (23), (24),
                (50), (51), (52), (53), (54), (55), (56)
            ) AS seed("Code")
            WHERE NOT EXISTS (
                SELECT 1
                FROM "RolePermissions" existing
                WHERE existing."RoleId" = 1 AND existing."Code" = seed."Code"
            );
            """);

        context.Database.ExecuteSqlRaw(
            """
            SELECT setval(
                pg_get_serial_sequence('"Roles"', 'Id'),
                GREATEST((SELECT COALESCE(MAX("Id"), 1) FROM "Roles"), 1)
            );
            """);
    }
}
