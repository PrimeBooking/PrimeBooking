namespace PrimeBooking.Domain.Hotel.Events;

public record HotelCreatedEvent(
    HotelId Id,
    string Name,
    int Capacity, 
    ContactInformation ContactInformation, 
    ICollection<Facility> Facilities, 
    ICollection<Star>? Stars) : DomainEvent;