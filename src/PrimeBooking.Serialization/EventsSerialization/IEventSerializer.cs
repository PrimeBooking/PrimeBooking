namespace PrimeBooking.Serialization.EventsSerialization;

public interface IEventSerializer
{
    SerializationResult SerializeEvent(IDomainEvent domainEvent);
    DeserializationResult DeserializeEvent(ReadOnlySpan<byte> data, string eventType, string contentType);
}
