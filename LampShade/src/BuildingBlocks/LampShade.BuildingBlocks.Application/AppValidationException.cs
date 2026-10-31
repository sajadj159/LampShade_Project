namespace LampShade.BuildingBlocks.Application;

/// <summary>Raised for invalid client input that is detected below the controller (e.g. an upload that is not an image).</summary>
public class AppValidationException(string message) : Exception(message);
