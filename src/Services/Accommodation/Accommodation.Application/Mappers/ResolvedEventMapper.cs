using System.Runtime.Serialization;

namespace Accommodation.Application.Mappers;

public class ResolvedEventMapper(IEventSerializer eventSerializer, IMetadataSerializer metadataSerializer) : IResolvedEventMapper
{
    public StreamEvent ToStreamEvent(ResolvedEvent resolvedEvent)
    {
        DeserializationResult deserializationResult = eventSerializer.DeserializeEvent(resolvedEvent.Event.Data.Span, resolvedEvent.Event.EventType, resolvedEvent.Event.ContentType);
        
        return deserializationResult switch {
            SuccessfulDeserialization success => resolvedEvent.Event.EventType.StartsWith('$') ? null : AsStreamEvent(success.Payload),
            FailedToDeserialize failed => throw new SerializationException(
                $"Can't deserialize {resolvedEvent.Event.EventType}: {failed.Error}"
            ),
            _ => throw new SerializationException("Unknown deserialization result")
        };
        
        
        StreamEvent AsStreamEvent(object payload)
            => new(
                resolvedEvent.Event.EventId.ToGuid(),
                payload,
                metadataSerializer.DeserializeMetadata(resolvedEvent.Event.Data.Span),
                resolvedEvent.Event.ContentType,
                resolvedEvent.Event.EventNumber.ToInt64()
            );
    }
}
