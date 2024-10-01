namespace PrimeBooking.Domain.Common;

public interface IAggregateRoot<out TEntityId>
{
    TEntityId Id { get; }
    long Version { get; }
    IReadOnlyList<IDomainEvent> GetDomainEvents();
    void ClearDomainEvents();
    void RaiseDomainEvent(IDomainEvent domainEvent);
}