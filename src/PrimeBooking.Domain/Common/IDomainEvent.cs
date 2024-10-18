using System.Text.Json.Serialization;

namespace PrimeBooking.Domain.Common;

public interface IDomainEvent : INotification
{
    [JsonIgnore]
    public Metadata Metadata { get; init; }
};
