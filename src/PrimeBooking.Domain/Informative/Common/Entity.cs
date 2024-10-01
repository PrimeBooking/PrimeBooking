namespace PrimeBooking.Domain.Informative.Common;

public abstract class Entity<TEntityId>
{
    public TEntityId Id { get; init; }    
    private readonly List<IDomainEvent> _domainEvents = new();
    
    protected Entity(TEntityId id)
    {
        Id = id;
    }

    protected Entity()
    {
    }
    
    public IReadOnlyList<IDomainEvent> GetDomainEvents() => _domainEvents.ToList();

    public void ClearDomainEvents() => _domainEvents.Clear();

    protected void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}