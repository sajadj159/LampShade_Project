namespace LampShade.BuildingBlocks.Application;

/// <summary>Strips scripts and other unsafe markup from rich text before it is stored.</summary>
public interface IHtmlSanitizer
{
    string Sanitize(string html);
}
