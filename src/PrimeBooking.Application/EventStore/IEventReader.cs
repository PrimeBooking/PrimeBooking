namespace PrimeBooking.Application.EventStore;

public interface IEventReader<TAggregateRoot> where TAggregateRoot : class, IAggregateRoot<TypedId<Guid>>
{
    
}