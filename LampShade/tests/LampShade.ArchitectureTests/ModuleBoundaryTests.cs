using AwesomeAssertions;
using NetArchTest.Rules;
using Xunit;

namespace LampShade.ArchitectureTests;

/// <summary>
/// A module may use another module only through that module's Contracts assembly
/// (its <c>I{Module}ModuleApi</c>, DTOs and integration events).
/// </summary>
public class ModuleBoundaryTests
{
    [Theory]
    [MemberData(nameof(Modules.ModuleLayers), MemberType = typeof(Modules))]
    public void Module_DoesNotReferenceAnotherModulesInternalAssemblies(string module, string layer)
    {
        var forbidden = Modules.Names.Where(other => other != module)
            .SelectMany(other => Modules.InternalLayers.Select(internalLayer => $"{Modules.Prefix(other)}.{internalLayer}"))
            .ToHashSet();

        var references = Modules.Assembly(module, layer).GetReferencedAssemblies().Select(x => x.Name!);

        references.Where(forbidden.Contains).Should().BeEmpty(
            $"{Modules.Prefix(module)}.{layer} may only use other modules through their .Contracts assembly");
    }

    [Theory]
    [MemberData(nameof(Modules.ModuleLayers), MemberType = typeof(Modules))]
    public void Module_TypesDoNotDependOnAnotherModulesInternalNamespaces(string module, string layer)
    {
        var forbidden = Modules.Names.Where(other => other != module).SelectMany(Modules.InternalNamespacesOf).ToArray();

        var result = Types.InAssembly(Modules.Assembly(module, layer)).ShouldNot().HaveDependencyOnAny(forbidden).GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Theory]
    [MemberData(nameof(Modules.ModuleNames), MemberType = typeof(Modules))]
    public void Contracts_ExposeNoInternalsAndNoPersistence(string module)
    {
        var forbidden = Modules.Names.SelectMany(Modules.InternalNamespacesOf)
            .Append("Microsoft.EntityFrameworkCore")
            .Append("Microsoft.AspNetCore")
            .ToArray();

        var result = Types.InAssembly(Modules.Assembly(module, "Contracts")).ShouldNot().HaveDependencyOnAny(forbidden).GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Theory]
    [MemberData(nameof(Modules.ModuleNames), MemberType = typeof(Modules))]
    public void Contracts_ReferenceOnlyBuildingBlocks(string module)
    {
        var moduleReferences = Modules.Assembly(module, "Contracts").GetReferencedAssemblies()
            .Select(x => x.Name!)
            .Where(name => Modules.Names.Any(other => name.StartsWith(Modules.Prefix(other) + ".")));

        moduleReferences.Should().BeEmpty("contracts are a module's published language and must not drag other code along");
    }

    [Fact]
    public void Host_ComposesModulesOnlyThroughInfrastructureAndPresentation()
    {
        var host = System.Reflection.Assembly.Load("LampShade.Api");
        var allowed = Modules.Names.SelectMany(module => new[] { $"{Modules.Prefix(module)}.Infrastructure", $"{Modules.Prefix(module)}.Presentation" }).ToHashSet();

        var moduleReferences = host.GetReferencedAssemblies().Select(x => x.Name!)
            .Where(name => Modules.Names.Any(module => name.StartsWith(Modules.Prefix(module) + ".")));

        moduleReferences.Should().OnlyContain(name => allowed.Contains(name));
    }
}
