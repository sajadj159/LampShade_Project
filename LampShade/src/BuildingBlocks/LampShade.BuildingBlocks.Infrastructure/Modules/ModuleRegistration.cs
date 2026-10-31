using LampShade.BuildingBlocks.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LampShade.BuildingBlocks.Infrastructure.Modules;

public static class ModuleRegistration
{
    public const string ConnectionStringName = "LampShadeDb";

    /// <summary>Registers every module plus one MediatR pipeline that covers all module handlers.</summary>
    public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration, params IModule[] modules)
    {
        foreach (var module in modules)
            module.Register(services, configuration);

        services.AddSingleton<IReadOnlyCollection<IModule>>(modules);
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(modules.SelectMany(module => module.HandlerAssemblies).Distinct().ToArray());
            cfg.AddOpenBehavior(typeof(UnitOfWorkPipelineBehavior<,>));
            cfg.NotificationPublisherType = typeof(IsolatedNotificationPublisher);
        });
        return services;
    }

    /// <summary>
    /// Registers a module's DbContext in its own Postgres schema, with its own migrations history
    /// table, and wires the context into the unit of work and the outbox processor.
    /// </summary>
    public static IServiceCollection AddModuleDbContext<TContext>(this IServiceCollection services, IConfiguration configuration, string schema)
        where TContext : DbContext, IDbContext
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringName)
            ?? throw new InvalidOperationException($"Connection string '{ConnectionStringName}' is missing.");

        services.AddDbContext<TContext>(options => options.UseNpgsql(connectionString,
            npgsql => npgsql.MigrationsHistoryTable("__EFMigrationsHistory", schema)));
        services.AddScoped<IDbContext>(sp => sp.GetRequiredService<TContext>());
        services.AddHostedService<OutboxProcessor<TContext>>();
        return services;
    }

    public static async Task InitializeModulesAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
    {
        await using var scope = services.CreateAsyncScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(ModuleRegistration));
        foreach (var module in scope.ServiceProvider.GetRequiredService<IReadOnlyCollection<IModule>>())
        {
            logger.LogInformation("Initializing module {Module}", module.Name);
            await module.InitializeAsync(scope.ServiceProvider, cancellationToken);
        }
    }
}
