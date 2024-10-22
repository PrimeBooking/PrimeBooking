using System.Net;

namespace Accommodation.Infrastructure.EventStore.Repositories;

public class EventStoreRepository<TAggregateRoot>(EventStoreClient client, IEventDataMapper eventDataMapper, IResolvedEventMapper resolvedEventMapper)
    : IEventStoreRepository<TAggregateRoot>
    where TAggregateRoot : class, IAggregateRoot<TypedId<Guid>>
{
    public async Task<Result<AppendEventsResult>> AppendEventsAsync(TAggregateRoot aggregateRoot, long expectedVersion, CancellationToken cancellationToken = default)
    {
        IDomainEvent[] events = aggregateRoot.GetDomainEvents().ToArray();
        EventData[] payload = events.Select(eventDataMapper.ToEventData).ToArray();
        string streamName = GetStreamName(aggregateRoot, aggregateRoot.Id.Value);
        
        Result<AppendEventsResult> result = await TryExecute(
            async () =>
            {
                IWriteResult write = expectedVersion == 0
                    ? await client.AppendToStreamAsync(streamName, StreamState.NoStream, payload,
                        cancellationToken: cancellationToken)
                    : await client.AppendToStreamAsync(streamName, StreamRevision.FromInt64(expectedVersion), payload,
                        cancellationToken: cancellationToken);
                
                return new AppendEventsResult(write.LogPosition.CommitPosition, write.NextExpectedStreamRevision.ToInt64());
            },
            streamName);
        
        return result;
    }

    public async Task<Result<StreamEvent[]>> ReadEventsAsync(string streamName, CancellationToken cancellationToken = default)
    {
        EventStoreClient.ReadStreamResult read = client.ReadStreamAsync(Direction.Forwards, streamName, new StreamPosition(StreamRevision.FromInt64(0L)), cancellationToken: cancellationToken);
        
        return await TryExecute(
            async () => {
                ResolvedEvent[] resolvedEvents = await read.ToArrayAsync(cancellationToken);

                return resolvedEvents.Select(resolvedEventMapper.ToStreamEvent).ToArray();
            },
            streamName
        );
    }

    public async Task<bool> StreamExists(string stream, CancellationToken cancellationToken = default)
    {
        EventStoreClient.ReadStreamResult read = client.ReadStreamAsync(Direction.Backwards, stream, StreamPosition.End, 1, cancellationToken: cancellationToken);

        ReadState readState = await read.ReadState;

        return readState == ReadState.Ok;
    }

    public string GetStreamName(TAggregateRoot aggregateRoot, Guid aggregateId) => 
        $"{aggregateRoot.GetType().Name}-{aggregateId}";
    
    private async Task<Result<T>> TryExecute<T>(Func<Task<T>> func, string stream) 
    {
        try 
        {
            return Result.Success(await func());
        } 
        catch (Exception ex)
        {
            //logging with "stream?"
            Error error = ex.BuildExceptionError(ErrorCode.UnhandledRequest, ErrorType.InternalServerError,
                HttpStatusCode.InternalServerError);
            return Result.Failure<T>(error);
        } 
    }
}
