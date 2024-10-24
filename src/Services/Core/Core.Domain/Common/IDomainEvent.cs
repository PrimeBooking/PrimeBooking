namespace Core.Domain.Common;

public interface IDomainEvent : INotification
{
    [JsonIgnore]
    public Metadata Metadata { get; init; }
};
