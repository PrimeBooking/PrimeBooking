using PrimeBooking.Domain.Hotel;
using PrimeBooking.Domain.Hotel.Events;

namespace Accommodation.Domain.Hotel;

public sealed class Hotel : AggregateRoot<HotelId>
{
    public string Name { get; private set; }
    public int Capacity { get; private set; }
    public ContactInformation ContactInformation { get; private set; }
    public ICollection<Facility> Facilities { get; private set; }
    public ICollection<Star>? Stars { get; private set; }
    
    public static Result<Hotel> Create(HotelId hotelId, string name, int capacity, ContactInformation contactInformation, ICollection<Facility> facilities, ICollection<Star>? stars)
    {
        if (hotelId.Id == Guid.Empty)
            return Result.Failure<Hotel>(HotelErrors.EmptyValue("Guid can't be empty"));
        
        if (string.IsNullOrEmpty(name)) return Result.Failure<Hotel>(HotelErrors.EmptyValue("Hotel Name can't be null or empty"));
        
        if (capacity <= 0) return Result.Failure<Hotel>(HotelErrors.LessZeroCapacityValue);

        Result<ContactInformation> contactInformationResult = ContactInformation.Create(contactInformation.Phone, contactInformation.Email,
            contactInformation.Address);
        
        if (contactInformationResult.IsFailure) 
            return Result.Failure<Hotel>(contactInformationResult.Error ?? throw new ArgumentNullException(nameof(contactInformationResult.Error), "Error is nullable"));
        
        var hotel = new Hotel(hotelId, name, capacity, contactInformationResult.Value!, facilities, stars);
        
        var @event = new HotelCreatedEvent(hotelId, name, capacity, contactInformationResult.Value!, facilities, stars);
        hotel.RaiseDomainEvent(@event);

        return Result.Success(hotel);
    }

    public Result Delete(HotelId hotelId)
    {
        if (hotelId.Id == Guid.Empty)
            return Result.Failure(HotelErrors.EmptyValue("Guid can't be empty"));

        var @event = new HotelDeletedEvent(hotelId);
        RaiseDomainEvent(@event);

        return Result.Success();
    }
    
    public Result<Hotel> UpdateContactInformation(ContactInformation contactInformation)
    {
        Result<ContactInformation> contactInformationResult = ContactInformation.Create(contactInformation.Phone, contactInformation.Email,
            contactInformation.Address);
        
        if (contactInformationResult.IsFailure) 
            return Result.Failure<Hotel>(contactInformationResult.Error ?? throw new ArgumentNullException(nameof(contactInformationResult.Error), "Error is nullable"));
        
        ContactInformation = contactInformation;
        
        var @event = new ContactInformationUpdatedEvent(contactInformation);
        RaiseDomainEvent(@event);

        return Result.Success(this);
    }
    
    public Result<Hotel> UpdateCapacity(int capacity)
    {
        if (capacity <= 0) return Result.Failure<Hotel>(HotelErrors.LessZeroCapacityValue);
        
        Capacity = capacity;
        
        var @event = new CapacityUpdatedEvent(capacity);
        RaiseDomainEvent(@event);

        return Result.Success(this);
    }
    
    public Result<Hotel> AddFacility(Facility facility)
    {
        Facilities.Add(facility);
        
        var @event = new FacilityAddedEvent(facility);
        RaiseDomainEvent(@event);

        return Result.Success(this);
    }
    
    public Result<Hotel> UpdateFacilities(ICollection<Facility>? facilities)
    {
        if (facilities is null || facilities.Count == 0) return Result.Failure<Hotel>(HotelErrors.EmptyValue("Facilities can't be empty or null"));
        
        Facilities = facilities;
        
        var @event = new FacilitiesUpdatedEvent(facilities);
        RaiseDomainEvent(@event);

        return Result.Success(this);
    }
    
    public Result<Hotel> AddStar(Star star)
    {
        Stars?.Add(star);

        var @event = new StarAddedEvent(star);
        RaiseDomainEvent(@event);

        return Result.Success(this);
    }

    public Result<Hotel> UpdateStars(ICollection<Star>? stars)
    {
        if (stars is null || stars.Count == 0) return Result.Failure<Hotel>(HotelErrors.EmptyValue("Stars can't be empty or null"));
        
        Stars = stars;
        
        var @event = new StarsUpdatedEvent(stars);
        RaiseDomainEvent(@event);

        return Result.Success(this);
    }
    
    private Hotel() { }
    
    private Hotel(HotelId hotelId, string name, int capacity, ContactInformation contactInformation, ICollection<Facility> facilities, ICollection<Star>? stars)
    {
        Id = hotelId;
        Name = name;
        Capacity = capacity;
        ContactInformation = contactInformation;
        Facilities = facilities;
        Stars = stars;
    }
}
