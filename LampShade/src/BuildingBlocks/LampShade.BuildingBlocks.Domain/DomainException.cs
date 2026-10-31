namespace LampShade.BuildingBlocks.Domain;

/// <summary>Raised when an aggregate refuses an operation that would break one of its rules.</summary>
public class DomainException(string message) : Exception(message);
