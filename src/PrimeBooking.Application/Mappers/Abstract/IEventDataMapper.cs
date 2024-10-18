namespace PrimeBooking.Application.Mappers.Abstract;

public interface IEventDataMapper
{
    EventData ToEventData(IDomainEvent domainEvent);
}
