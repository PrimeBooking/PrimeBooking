namespace PrimeBooking.Domain.Hotel.Events;

public record StarAddedEvent(Star Star) : DomainEvent;