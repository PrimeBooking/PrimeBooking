namespace Accommodation.Application.Mappers;

public class EventDataMapper(IEventSerializer eventSerializer, IMetadataSerializer metadataSerializer) : IEventDataMapper
{
    public EventData ToEventData(IDomainEvent domainEvent)
    {
        SerializationResult serializedEvent = eventSerializer.SerializeEvent(domainEvent);
        
        string eventType = domainEvent.GetType().Name;
        byte[] eventData = serializedEvent.Payload;
        byte[] metadata = metadataSerializer.SerializeMetadata(domainEvent.Metadata);
        string contentType = serializedEvent.ContentType;

        return new(
            Uuid.NewUuid(),
            eventType,
            eventData,
            metadata,
            contentType
        );
    }
}
