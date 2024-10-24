namespace Core.Domain.Common;

public record DomainEvent : IDomainEvent
{
    public DateTime TimeStamp { get; init; } = DateTime.UtcNow;
    
    [JsonIgnore]
    public Metadata Metadata { get; init; } = new();
}
