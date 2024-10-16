namespace PrimeBooking.Serialization.EventsSerialization;

public class EventSerializer : BaseSerializer, IEventSerializer
{
    public SerializationResult SerializeEvent(IDomainEvent domainEvent)
    {
        byte[] result = JsonSerializer.SerializeToUtf8Bytes(domainEvent, Options);

       return new SerializationResult(domainEvent.GetType().Name, ContentType, result);
    }

    public DeserializationResult DeserializeEvent(ReadOnlySpan<byte> data, string eventType, string contentType)
    {
        throw new NotImplementedException();
    }
    
    private static string ContentType => "application/json";
}
