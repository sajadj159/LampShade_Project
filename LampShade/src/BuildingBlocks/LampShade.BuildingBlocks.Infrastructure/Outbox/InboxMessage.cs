using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LampShade.BuildingBlocks.Infrastructure.Outbox;

/// <summary>Records that a consumer module already handled an integration event, so redelivery is a no-op.</summary>
public sealed class InboxMessage
{
    public Guid EventId { get; private set; }
    public string Handler { get; private set; } = string.Empty;
    public DateTime ProcessedOn { get; private set; }

    private InboxMessage()
    {
    }

    public InboxMessage(Guid eventId, string handler)
    {
        EventId = eventId;
        Handler = handler;
        ProcessedOn = DateTime.UtcNow;
    }
}

internal sealed class InboxMessageConfiguration : IEntityTypeConfiguration<InboxMessage>
{
    public void Configure(EntityTypeBuilder<InboxMessage> builder)
    {
        builder.ToTable("InboxMessages");
        builder.HasKey(x => new { x.EventId, x.Handler });
        builder.Property(x => x.Handler).HasMaxLength(300);
    }
}

public static class OutboxModelBuilderExtensions
{
    /// <summary>Adds the outbox and inbox tables to a module's schema.</summary>
    public static ModelBuilder ApplyOutboxAndInbox(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
        return modelBuilder;
    }
}
