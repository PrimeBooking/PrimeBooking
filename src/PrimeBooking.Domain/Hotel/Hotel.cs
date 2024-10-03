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
        if (string.IsNullOrEmpty(name)) return Result.Failure<Hotel>(HotelErrors.EmptyValue("Hotel Name can't be null or empty"));
        
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
            return Result.Failure(HotelErrors.EmptyValue("Guid can't be empty"));

        var @event = new HotelDeletedEvent(Id);
        RaiseDomainEvent(@event);

        return Result.Success();
    }
    
    public Result<Hotel> UpdateCapacity(int capacity)
    {
        if (capacity <= 0) return Result.Failure<Hotel>(HotelErrors.LessZeroCapacityValue);
        
        var hotel = new Hotel(Name, capacity, ContactInformation, Facilities, Stars);
        
        var @event = new CapacityUpdatedEvent(capacity);
        RaiseDomainEvent(@event);

        return Result.Success(hotel);
    }

    public Result<Hotel> UpdateStars(ICollection<Star>? stars)
    {
        if (stars is null || stars.Count == 0) return Result.Failure<Hotel>(HotelErrors.EmptyValue("Stars can't be empty or null"));
        
        var hotel = new Hotel(Name, Capacity, ContactInformation, Facilities, stars);
        
        var @event = new StarsUpdatedEvent(stars);
        RaiseDomainEvent(@event);

        return Result.Success(hotel);
    }
    
    public Result<Hotel> UpdateContactInformation(ContactInformation contactInformation)
    {
        var contactInformationResult = ContactInformation.Create(contactInformation.Phone, contactInformation.Email,
            contactInformation.Address);
        
        if (contactInformationResult.IsFailure) 
            return Result.Failure<Hotel>(contactInformationResult.Error ?? throw new ArgumentNullException(nameof(contactInformationResult.Error), "Error is nullable"));
        
        var hotel = new Hotel(Name, Capacity, contactInformation, Facilities, Stars);
        
        var @event = new ContactInformationUpdatedEvent(contactInformation);
        RaiseDomainEvent(@event);

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