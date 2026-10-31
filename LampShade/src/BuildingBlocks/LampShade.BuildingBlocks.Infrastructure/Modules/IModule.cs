using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LampShade.BuildingBlocks.Infrastructure.Modules;

/// <summary>
/// The single entry point of a module. The host only knows modules through this interface:
/// it lets each module register its own services, hands MediatR the module's assemblies,
/// and runs the module's start-up work (migrations, seed data).
/// </summary>
public interface IModule
{
    string Name { get; }

    /// <summary>Assemblies holding the module's MediatR handlers (commands, queries, event handlers).</summary>
    IEnumerable<Assembly> HandlerAssemblies { get; }

    void Register(IServiceCollection services, IConfiguration configuration);

    Task InitializeAsync(IServiceProvider services, CancellationToken cancellationToken);
}
