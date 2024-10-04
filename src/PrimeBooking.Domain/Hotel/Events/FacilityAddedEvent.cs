namespace PrimeBooking.Domain.Hotel.Events;

public record FacilityAddedEvent(Facility Facility) : DomainEvent;