namespace PrimeBooking.Domain.Hotel.Events;

public record StarsUpdatedEvent(ICollection<Star> Stars) : DomainEvent;