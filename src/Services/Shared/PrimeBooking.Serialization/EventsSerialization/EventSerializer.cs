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
        if (contentType != ContentType) return new FailedToDeserialize(SerializationErrors.FailedDeserialization("Content type is not matched"));
        
        Type type = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .FirstOrDefault(t => t.Name == eventType);
        
        object? deserialized = JsonSerializer.Deserialize(data, type!, Options);

        return deserialized != null
            ? new SuccessfulDeserialization(deserialized)
            : new FailedToDeserialize(SerializationErrors.FailedDeserialization("Payload is empty"));
    }
    
    private static string ContentType => "application/json";
}
