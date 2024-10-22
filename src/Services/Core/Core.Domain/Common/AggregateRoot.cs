namespace Core.Domain.Common;

public class AggregateRoot<TEntityId> : Entity<TEntityId>, IAggregateRoot<TEntityId> 
    where TEntityId : TypedId<Guid>
{
    public long Version { get; protected set; }
    public IReadOnlyList<IDomainEvent> GetDomainEvents() => _domainEvents.ToList();
    public void ClearDomainEvents() => _domainEvents.Clear();
    public void RaiseDomainEvent(IDomainEvent @event) => _domainEvents.Add(@event);
    
    [JsonIgnore]
    private readonly List<IDomainEvent> _domainEvents = new();
}
