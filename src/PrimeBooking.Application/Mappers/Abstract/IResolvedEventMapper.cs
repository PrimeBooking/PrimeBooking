namespace PrimeBooking.Application.Mappers.Abstract;

public interface IResolvedEventMapper
{
    StreamEvent ToStreamEvent(ResolvedEvent resolvedEvent);
}
