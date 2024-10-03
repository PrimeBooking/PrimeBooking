namespace PrimeBooking.Domain.Hotel.Events;

public record FacilitiesUpdatedEvent(ICollection<Facility> Facilities) : DomainEvent;