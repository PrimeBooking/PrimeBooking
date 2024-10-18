namespace PrimeBooking.Application.EventStore;

public interface IEventWriter<TAggregateRoot> where TAggregateRoot : class, IAggregateRoot<TypedId<Guid>>
{
    Task<Result<AppendEventsResult>> AppendEventsAsync(TAggregateRoot aggregateRoot,
        long expectedVersion, CancellationToken cancellationToken = default);
}
