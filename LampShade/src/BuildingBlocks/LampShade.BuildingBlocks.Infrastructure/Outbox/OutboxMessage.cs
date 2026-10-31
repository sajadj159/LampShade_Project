using LampShade.BuildingBlocks.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace LampShade.BuildingBlocks.Infrastructure.Outbox;

/// <summary>An integration event waiting in a module's schema to be published.</summary>
public sealed class OutboxMessage
{
    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web);

    public Guid Id { get; private set; }
    public string Type { get; private set; } = string.Empty;
    public string Payload { get; private set; } = string.Empty;
    public DateTime OccurredOn { get; private set; }
    public DateTime? ProcessedOn { get; private set; }
    public int Attempts { get; private set; }
    public string? Error { get; private set; }

    private OutboxMessage()
    {
    }

    public static OutboxMessage From(IntegrationEvent integrationEvent)
    {
        var type = integrationEvent.GetType();
        return new OutboxMessage
        {
            Id = integrationEvent.Id,
            Type = $"{type.FullName}, {type.Assembly.GetName().Name}",
            Payload = JsonSerializer.Serialize(integrationEvent, type, SerializerOptions),
            OccurredOn = integrationEvent.OccurredOn
        };
    }

    public IntegrationEvent ToIntegrationEvent()
    {
        var type = System.Type.GetType(Type, throwOnError: true)!;
        return (IntegrationEvent)JsonSerializer.Deserialize(Payload, type, SerializerOptions)!;
    }

    public void MarkProcessed()
    {
        ProcessedOn = DateTime.UtcNow;
        Error = null;
    }

    public void MarkFailed(string error)
    {
        Attempts++;
        Error = error.Length > 2000 ? error[..2000] : error;
    }
}

internal sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("OutboxMessages");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.Type).HasMaxLength(500).IsRequired();
        builder.Property(x => x.Payload).IsRequired();
        builder.Property(x => x.Error).HasMaxLength(2000);
        builder.HasIndex(x => x.ProcessedOn);
    }
}
