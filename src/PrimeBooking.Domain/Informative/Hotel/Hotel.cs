using PrimeBooking.Domain.Informative.Hotel.Errors;
using PrimeBooking.Domain.Informative.Hotel.Events;

namespace PrimeBooking.Domain.Informative.Hotel;

public sealed class Hotel : AggregateRoot<HotelId>
{
    public string Name { get; private set; }
    public int Capacity { get; private set; }
    public ContactInformation ContactInformation { get; private set; }
    public ICollection<Facility> Facilities { get; private set; }
    public ICollection<Star> Stars { get; private set; }
    
    private Hotel()
    {
    }

    public Hotel(string name, int capacity, ContactInformation contactInformation, ICollection<Facility> facilities, ICollection<Star> stars)
    {
        Name = name;
        Capacity = capacity;
        ContactInformation = contactInformation;
        Facilities = facilities;
        Stars = stars;
    }

    public Result Create()
    {
        if (string.IsNullOrEmpty(Name)) return Result.Failure(HotelErrors.EmptyNameValue);
        
        if (Capacity <= 0) return Result.Failure(HotelErrors.LessZeroCapacityValue);
        
        RaiseDomainEvent(new CreatedEvent(Name, Capacity, ContactInformation, Facilities, Stars, DateTime.Now));

        return Result.Success();
    }
}