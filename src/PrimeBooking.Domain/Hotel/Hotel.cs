using PrimeBooking.Domain.Hotel.Errors;
using PrimeBooking.Domain.Hotel.Events;

namespace PrimeBooking.Domain.Hotel;

public sealed class Hotel : AggregateRoot<HotelId>
{
    public string Name { get; private set; }
    public int Capacity { get; private set; }
    public ContactInformation ContactInformation { get; private set; }
    public ICollection<Facility> Facilities { get; private set; }
    public ICollection<Star>? Stars { get; private set; }
    
    public Result<Hotel> Create(string name, int capacity, ContactInformation contactInformation, ICollection<Facility> facilities, ICollection<Star>? stars)
    {
        if (string.IsNullOrEmpty(name)) return Result.Failure<Hotel>(HotelErrors.EmptyNameValue);
        
        if (Capacity <= 0) return Result.Failure<Hotel>(HotelErrors.LessZeroCapacityValue);

        var contactInformationResult = ContactInformation.Create(contactInformation.Phone, contactInformation.Email,
            contactInformation.Address);
        
        if (contactInformationResult.IsFailure) 
            return Result.Failure<Hotel>(contactInformationResult.Error ?? throw new ArgumentNullException(nameof(contactInformationResult.Error), "Error is nullable"));
        
        var hotel = new Hotel(name, capacity, contactInformationResult.Value, facilities, stars);
        
        var @event = new HotelCreatedEvent(name, Capacity, ContactInformation, Facilities, Stars);
        RaiseDomainEvent(@event);

        return Result.Success(hotel);
    }

    public Result Delete(HotelId hotelId)
    {
        if (hotelId.Id == Guid.Empty)
            return Result.Failure(HotelErrors.EmptyGuidValue);
        
        RaiseDomainEvent(new HotelDeletedEvent(Id));

        return Result.Success();
    }

    public Result<Hotel> UpdateStars(ICollection<Star>? stars)
    {
        if (stars is null || stars.Count == 0) return Result.Failure<Hotel>(HotelErrors.EmptyNameValue);
        
        var hotel = new Hotel(Name, Capacity, ContactInformation, Facilities, stars);
        
        RaiseDomainEvent(new StarsUpdatedEvent(stars));

        return Result.Success(hotel);
    }
    
    private Hotel() { }
    
    private Hotel(string name, int capacity, ContactInformation contactInformation, ICollection<Facility> facilities, ICollection<Star>? stars)
    {
        Name = name;
        Capacity = capacity;
        ContactInformation = contactInformation;
        Facilities = facilities;
        Stars = stars;
    }
}