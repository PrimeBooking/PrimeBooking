using Accommodation.Application.EventStore;

namespace Accommodation.Application.Repositories;

public interface IEventStoreRepository<TAggregateRoot> : IEventWriter<TAggregateRoot>, IEventReader<TAggregateRoot>
    where TAggregateRoot : class, IAggregateRoot<TypedId<Guid>>
{
    Task<bool> StreamExists(string stream, CancellationToken cancellationToken = default);
    string GetStreamName(TAggregateRoot aggregateRoot, Guid aggregateId);
}
