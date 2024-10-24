using Accommodation.Domain.Hotel;

namespace PrimeBooking.Domain.Hotel.Events;

public record HotelCreatedEvent : DomainEvent
{
    public HotelId Id { get; init; }
    public string Name { get; init; }
    public int Capacity { get; init; } 
    public ContactInformation ContactInformation { get; init; }
    public ICollection<Facility> Facilities { get; init; }
    public ICollection<Star>? Stars { get; init; }

    [JsonConstructor]
    public HotelCreatedEvent(HotelId id, string name, int capacity, ContactInformation contactInformation, ICollection<Facility> facilities, ICollection<Star>? stars)
    {
        Id = id;
        Name = name;
        Capacity = capacity;
        ContactInformation = contactInformation;
        Facilities = facilities;
        Stars = stars;
    }
}
