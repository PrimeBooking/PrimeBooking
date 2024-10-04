namespace PrimeBooking.Domain.Common;

public record DomainEvent : IDomainEvent
{
    public DateTime TimeStamp { get; init; } = DateTime.UtcNow;
}