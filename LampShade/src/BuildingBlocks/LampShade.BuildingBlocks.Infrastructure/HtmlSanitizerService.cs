using Ganss.Xss;
using LampShade.BuildingBlocks.Application;

namespace LampShade.BuildingBlocks.Infrastructure;

public sealed class HtmlSanitizerService : LampShade.BuildingBlocks.Application.IHtmlSanitizer
{
    private readonly HtmlSanitizer _sanitizer = new();

    public string Sanitize(string html) => string.IsNullOrWhiteSpace(html) ? html : _sanitizer.Sanitize(html);
}
