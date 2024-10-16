namespace PrimeBooking.Infrastructure.EventStore.Repositories;

public class EventStoreRepository<TAggregateRoot>(EventStoreClient client, IEventDataMapper mapper)
    : IEventStoreRepository<TAggregateRoot>
    where TAggregateRoot : class, IAggregateRoot<TypedId<Guid>>
{
    public async Task<Result<AppendEventsResult>> AppendEventsAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default)
    {
        IDomainEvent[] events = aggregateRoot.GetDomainEvents().ToArray();
        IEnumerable<EventData> payload = events.Select(mapper.ToEventData);
        string streamName = GetStreamName(aggregateRoot, aggregateRoot.Id.Value);
        
        // add try-catch, any-or-not, errors?
        IWriteResult write = await client.AppendToStreamAsync(streamName, StreamState.Any, payload, cancellationToken: cancellationToken); AppendEventsResult result = new(write.LogPosition.CommitPosition, write.NextExpectedStreamRevision.ToInt64());
        
        return Result.Success(result);
    }

    // public async Task<Result<StreamEvent[]>> ReadEventsAsync(CancellationToken cancellationToken = default)
    // {
    //     
    // }

    public async Task<bool> StreamExists(string stream, CancellationToken cancellationToken = default)
    {
        EventStoreClient.ReadStreamResult read = client.ReadStreamAsync(Direction.Backwards, stream, StreamPosition.End, 1, cancellationToken: cancellationToken);

        ReadState readState = await read.ReadState;

        return readState == ReadState.Ok;
    }

    public string GetStreamName(TAggregateRoot aggregateRoot, Guid aggregateId) => 
        $"{aggregateRoot.GetType().Name}-{aggregateId}";
}
