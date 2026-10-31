# LampShade backend: a modular monolith

One deployable ASP.NET Core API (`src/Host/LampShade.Api`) made of six modules that could each become a service of its own:

| Module | Owns | Schema |
|---|---|---|
| Shop | catalog (products, categories, pictures, slides), cart pricing, orders | `shop` |
| Inventory | stock and unit prices | `inventory` |
| Discount | customer and colleague discounts | `discount` |
| Blog | articles and categories | `blog` |
| Comment | comments on products and articles | `comment` |
| Account | accounts, roles, permissions, buyer notifications | `account` |

## Layout

```
src/
  BuildingBlocks/              shared kernel: entity base, OperationResult, auth/file/SMS abstractions,
                               IModule, outbox/inbox, unit of work, storage
  Modules/<Name>/
    <Name>Management.Contracts       public: I<Name>ModuleApi, DTOs, integration events
    <Name>Management.Domain          aggregates and repository interfaces
    <Name>Management.Application     commands, queries, handlers (MediatR)
    <Name>Management.Infrastructure  DbContext, migrations, repositories, query services,
                                     module API implementation, event handlers, <Name>Module installer
    <Name>Management.Presentation    controllers (routes unchanged: api/read/*, api/write/*)
  Host/LampShade.Api            composition root only
tests/
  <Name>/<Name>Management.Tests     domain unit tests
  LampShade.IntegrationTests        boots every module against PostgreSQL (Testcontainers)
  LampShade.ArchitectureTests       fails when a module reaches into another module's internals
```

## Rules

- A module uses another module **only through its `.Contracts` assembly**: synchronous reads through
  `I<Name>ModuleApi` (e.g. Shop asks Inventory for prices and stock), side effects through integration
  events (e.g. `OrderPaidIntegrationEvent` → Inventory reduces stock, Account texts the buyer).
- A command changes **one** module's data. The unit of work refuses a command that changed two DbContexts.
- Integration events are written to the publisher's `OutboxMessages` table in the same transaction as its
  data and delivered by a background `OutboxProcessor`. Subscribers are idempotent (`InboxMessages`),
  isolated from each other, and retried until they succeed (10 attempts; the error is kept on the row).
- Each module has its own schema and `__EFMigrationsHistory`; there are no cross-schema foreign keys.
- `tests/LampShade.ArchitectureTests` enforces these rules on every build in CI.

## Running locally

```bash
docker compose up --build
```

API on http://localhost:5002 (Swagger at `/swagger`), PostgreSQL on 5432, SeaweedFS (S3) on 8333.
Modules migrate their own schema on start-up. Account seeds the roles and, when
`Account:BootstrapAdmin:UserName/Password` is configured (docker-compose and
`appsettings.Development.json` set `admin` / `Admin@12345` for development), creates the first administrator.
Self-registration always creates a regular user.

Upgrading an existing development database: the tables now live in the module schemas, so the old
tables in `public` are no longer used. Drop them, or start fresh with `docker compose down -v`.

## Adding a migration

```bash
dotnet ef migrations add <Name> -p src/Modules/Shop/ShopManagement.Infrastructure -s src/Host/LampShade.Api -c ShopContext -o Migrations
```

## Tests

```bash
dotnet test LampShade.sln
```

Integration tests need Docker running.
