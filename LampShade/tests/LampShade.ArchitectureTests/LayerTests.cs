using AwesomeAssertions;
using LampShade.BuildingBlocks.Infrastructure.Modules;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetArchTest.Rules;
using Xunit;

namespace LampShade.ArchitectureTests;

/// <summary>Inside a module, dependencies point inwards: Presentation/Infrastructure → Application → Domain.</summary>
public class LayerTests
{
    [Theory]
    [MemberData(nameof(Modules.ModuleNames), MemberType = typeof(Modules))]
    public void Domain_DependsOnNoOuterLayerAndNoFramework(string module)
    {
        var prefix = Modules.Prefix(module);

        var result = Types.InAssembly(Modules.Assembly(module, "Domain")).ShouldNot().HaveDependencyOnAny(
                $"{prefix}.Application", $"{prefix}.Infrastructure", $"{prefix}.Presentation", $"{prefix}.Contracts",
                "Microsoft.EntityFrameworkCore", "Microsoft.AspNetCore", "MediatR")
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Theory]
    [MemberData(nameof(Modules.ModuleNames), MemberType = typeof(Modules))]
    public void Application_DoesNotKnowAboutInfrastructureOrPersistence(string module)
    {
        var prefix = Modules.Prefix(module);

        var result = Types.InAssembly(Modules.Assembly(module, "Application")).ShouldNot().HaveDependencyOnAny(
                $"{prefix}.Infrastructure", $"{prefix}.Presentation", "Microsoft.EntityFrameworkCore", "LampShade.BuildingBlocks.Infrastructure")
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Theory]
    [MemberData(nameof(Modules.ModuleNames), MemberType = typeof(Modules))]
    public void Presentation_TalksToTheModuleOnlyThroughMediatR(string module)
    {
        var prefix = Modules.Prefix(module);

        var result = Types.InAssembly(Modules.Assembly(module, "Presentation")).ShouldNot().HaveDependencyOnAny(
                $"{prefix}.Domain", $"{prefix}.Infrastructure", "Microsoft.EntityFrameworkCore")
            .GetResult();

        result.FailingTypeNames.Should().BeNullOrEmpty();
    }

    [Theory]
    [MemberData(nameof(Modules.ModuleNames), MemberType = typeof(Modules))]
    public void Controllers_LiveOnlyInPresentation(string module)
    {
        foreach (var layer in Modules.AllLayers.Where(layer => layer != "Presentation"))
            Modules.Assembly(module, layer).GetTypes().Should().NotContain(type => typeof(ControllerBase).IsAssignableFrom(type), $"{layer} must not host controllers");

        Types.InAssembly(Modules.Assembly(module, "Presentation")).That().Inherit(typeof(ControllerBase))
            .Should().HaveNameEndingWith("Controller").GetResult().IsSuccessful.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(Modules.ModuleNames), MemberType = typeof(Modules))]
    public void DbContexts_LiveOnlyInInfrastructure(string module)
    {
        foreach (var layer in Modules.AllLayers.Where(layer => layer != "Infrastructure"))
            Modules.Assembly(module, layer).GetTypes().Should().NotContain(type => typeof(DbContext).IsAssignableFrom(type));

        Modules.Assembly(module, "Infrastructure").GetTypes().Where(type => typeof(DbContext).IsAssignableFrom(type))
            .Should().ContainSingle("each module owns exactly one schema");
    }

    [Theory]
    [MemberData(nameof(Modules.ModuleNames), MemberType = typeof(Modules))]
    public void EveryModule_HasOneInstaller(string module) =>
        Modules.Assembly(module, "Infrastructure").GetTypes().Where(type => typeof(IModule).IsAssignableFrom(type) && !type.IsAbstract)
            .Should().ContainSingle();

    [Theory]
    [MemberData(nameof(Modules.ModuleNames), MemberType = typeof(Modules))]
    public void RequestHandlers_LiveInTheirModule(string module)
    {
        var handlerInterfaces = new[] { typeof(IRequestHandler<,>), typeof(IRequestHandler<>) };
        var prefix = Modules.Prefix(module);

        var handlers = Modules.InternalLayers.SelectMany(layer => Modules.Assembly(module, layer).GetTypes())
            .Where(type => !type.IsAbstract && type.GetInterfaces().Any(i => i.IsGenericType && handlerInterfaces.Contains(i.GetGenericTypeDefinition())));

        foreach (var handler in handlers)
        {
            var requestType = handler.GetInterfaces().First(i => i.IsGenericType && handlerInterfaces.Contains(i.GetGenericTypeDefinition())).GetGenericArguments()[0];
            requestType.Assembly.GetName().Name.Should().StartWith(prefix, $"{handler.Name} handles a request that belongs to another module");
        }
    }
}
