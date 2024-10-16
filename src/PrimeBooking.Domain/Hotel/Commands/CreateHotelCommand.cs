namespace PrimeBooking.Domain.Hotel.Commands;

public record CreateHotelCommand : IRequest
{
    public string Name { get; set; }
    public int Capacity { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public ICollection<Facility> Facilities { get; set; }
    public ICollection<Star>? Stars { get; set; }
    
    // public static Result<CreateHotelCommand> Create(HotelId hotelId, string name, int capacity, ContactInformation contactInformation, ICollection<Facility> facilities, ICollection<Star>? stars)
    // {
    //     Result<ContactInformation> contactInformationResult = ContactInformation.Create(contactInformation.Phone, contactInformation.Email,
    //         contactInformation.Address);
    //     
    //     var command = new CreateHotelCommand(hotelId, name, capacity, contactInformationResult.Value!, facilities, stars);
    //     
    //     return Result.Success(command);
    // }
    //
    // private CreateHotelCommand(HotelId hotelId, string name, int capacity, ContactInformation contactInformation, ICollection<Facility> facilities, ICollection<Star>? stars)
    // {
    //     Id = hotelId;
    //     Name = name;
    //     Capacity = capacity;
    //     ContactInformation = contactInformation;
    //     Facilities = facilities;
    //     Stars = stars;
    // }
};
