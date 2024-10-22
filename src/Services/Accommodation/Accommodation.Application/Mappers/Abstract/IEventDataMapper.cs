namespace Accommodation.Application.Mappers.Abstract;

public interface IEventDataMapper
{
    EventData ToEventData(IDomainEvent domainEvent);
}
