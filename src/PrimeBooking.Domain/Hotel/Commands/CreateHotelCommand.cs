namespace PrimeBooking.Domain.Hotel.Commands;

public record CreateHotelCommand : IRequest
{
    public string Name { get; init; }
    public int Capacity { get; init; }
    public string Email { get; init; }
    public string Phone { get; init; }
    public ICollection<Facility> Facilities { get; init; }
    public ICollection<Star>? Stars { get; init; }
};
