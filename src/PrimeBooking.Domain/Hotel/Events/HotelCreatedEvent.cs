namespace PrimeBooking.Domain.Hotel.Events;

public record HotelCreatedEvent(
    string Name,
    int Capacity, 
    ContactInformation ContactInformation, 
    ICollection<Facility> Facilities, 
    ICollection<Star>? Stars) : DomainEvent;