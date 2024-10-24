namespace PrimeBooking.Domain.Hotel.Events;

public record CapacityUpdatedEvent(int Capacity) : DomainEvent;