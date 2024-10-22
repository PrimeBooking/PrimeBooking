namespace Core.Domain.Common;

public interface IAggregateRoot<out TEntityId> where TEntityId : TypedId<Guid>
{
    TEntityId Id { get; }
    long Version { get; }
    IReadOnlyList<IDomainEvent> GetDomainEvents();
    void ClearDomainEvents();
    void RaiseDomainEvent(IDomainEvent domainEvent);
}
