namespace PrimeBooking.Domain.Informative.Hotel.Events;

public record CreatedEvent(
    string Name,
    int Capacity, 
    ContactInformation ContactInformation, 
    ICollection<Facility> Facilities, 
    ICollection<Star> Stars, 
    DateTime Time) : IHotelDomainEvent;