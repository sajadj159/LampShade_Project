using AccountManagement.Infrastructure;
using BlogManagement.Infrastructure;
using CommentManagement.Infrastructure;
using DiscountManagement.Infrastructure;
using InventoryManagement.Infrastructure;
using LampShade.BuildingBlocks.Application;
using LampShade.BuildingBlocks.Infrastructure;
using LampShade.BuildingBlocks.Infrastructure.Modules;
using LampShade.BuildingBlocks.Infrastructure.Outbox;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using ShopManagement.Infrastructure;
using Testcontainers.PostgreSql;
using Xunit;

namespace LampShade.IntegrationTests.Fixtures;

/// <summary>
/// Boots every module the way the host does (same <see cref="IModule"/> registrations, MediatR pipeline,
/// schemas and migrations) against a throw-away PostgreSQL container. Infrastructure that talks to the
/// outside world (file storage, SMS, the signed-in user) is replaced by test doubles.
/// </summary>
public sealed class ModularMonolithFixture : IAsyncLifetime
{
    static ModularMonolithFixture() => AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

    private readonly PostgreSqlContainer _container = new PostgreSqlBuilder("postgres:16-alpine").Build();

    public IServiceProvider Services { get; private set; } = default!;
    public TestAuthHelper Auth { get; } = new();
    public TestSmsService Sms { get; } = new();

    public async ValueTask InitializeAsync()
    {
        await _container.StartAsync();

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                [$"ConnectionStrings:{ModuleRegistration.ConnectionStringName}"] = _container.GetConnectionString()
            })
            .Build();

        var services = new ServiceCollection();
        services.AddLogging();
        services.AddSingleton<IConfiguration>(configuration);
        services.AddSingleton<IAuthHelper>(Auth);
        services.AddSingleton<ISmsService>(Sms);
        services.AddSingleton<IFileUploader, TestFileUploader>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IHtmlSanitizer, HtmlSanitizerService>();
        // Account is registered first on purpose: its OrderPaid subscriber then runs before Inventory's,
        // which is what the "one failing subscriber does not block the others" test needs.
        services.AddModules(configuration,
            new AccountModule(), new ShopModule(), new DiscountModule(), new InventoryModule(), new BlogModule(), new CommentModule());

        Services = services.BuildServiceProvider(new ServiceProviderOptions { ValidateScopes = true });
        await Services.InitializeModulesAsync();
    }

    public async ValueTask DisposeAsync()
    {
        if (Services is IAsyncDisposable disposable) await disposable.DisposeAsync();
        await _container.DisposeAsync();
    }

    /// <summary>Sends a request through the MediatR pipeline in its own scope, like one HTTP request.</summary>
    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        await using var scope = Services.CreateAsyncScope();
        return await scope.ServiceProvider.GetRequiredService<IMediator>().Send(request, TestContext.Current.CancellationToken);
    }

    public async Task<TResult> WithAsync<TService, TResult>(Func<TService, Task<TResult>> action) where TService : notnull
    {
        await using var scope = Services.CreateAsyncScope();
        return await action(scope.ServiceProvider.GetRequiredService<TService>());
    }

    public async Task WithAsync<TService>(Func<TService, Task> action) where TService : notnull
    {
        await using var scope = Services.CreateAsyncScope();
        await action(scope.ServiceProvider.GetRequiredService<TService>());
    }

    /// <summary>Delivers a module's pending integration events, as the background outbox processor would.</summary>
    public Task<int> ProcessOutboxAsync<TContext>() where TContext : DbContext =>
        new OutboxProcessor<TContext>(Services.GetRequiredService<IServiceScopeFactory>(), NullLogger<OutboxProcessor<TContext>>.Instance)
            .ProcessPendingAsync(TestContext.Current.CancellationToken);

    public static IFormFile Image(string name) =>
        new FormFile(new MemoryStream([1]), 0, 1, "file", name) { Headers = new HeaderDictionary(), ContentType = "image/jpeg" };

    public static string Unique(string prefix) => $"{prefix}-{Guid.NewGuid():N}";
}

[CollectionDefinition(nameof(ModularMonolithCollection))]
public sealed class ModularMonolithCollection : ICollectionFixture<ModularMonolithFixture>;
