namespace Accommodation.Application.EventStore;

public interface IEventReader<TAggregateRoot> where TAggregateRoot : class, IAggregateRoot<TypedId<Guid>>
{
    Task<Result<StreamEvent[]>> ReadEventsAsync(string streamName, CancellationToken cancellationToken = default);
}
