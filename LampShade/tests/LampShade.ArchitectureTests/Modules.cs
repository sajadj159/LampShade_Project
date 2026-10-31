using System.Reflection;
using Xunit;

namespace LampShade.ArchitectureTests;

/// <summary>Where every module's assemblies are, by naming convention: <c>{Name}Management.{Layer}</c>.</summary>
public static class Modules
{
    public static readonly string[] Names = ["Account", "Blog", "Comment", "Discount", "Inventory", "Shop"];

    /// <summary>Layers that are private to a module. Only <c>Contracts</c> may be used by other modules.</summary>
    public static readonly string[] InternalLayers = ["Domain", "Application", "Infrastructure", "Presentation"];

    public static readonly string[] AllLayers = ["Contracts", .. InternalLayers];

    public static string Prefix(string module) => $"{module}Management";

    public static Assembly Assembly(string module, string layer) => System.Reflection.Assembly.Load($"{Prefix(module)}.{layer}");

    public static IEnumerable<string> InternalNamespacesOf(string module) => InternalLayers.Select(layer => $"{Prefix(module)}.{layer}")
        // Permission exposers live in Infrastructure under this older namespace.
        .Append($"{Prefix(module)}.Configuration");

    public static TheoryData<string, string> ModuleLayers()
    {
        var data = new TheoryData<string, string>();
        foreach (var module in Names)
        foreach (var layer in AllLayers)
            data.Add(module, layer);
        return data;
    }

    public static TheoryData<string> ModuleNames() => new(Names);
}
