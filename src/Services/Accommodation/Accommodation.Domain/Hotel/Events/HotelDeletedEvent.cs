using Accommodation.Domain.Hotel;

namespace PrimeBooking.Domain.Hotel.Events;

public record HotelDeletedEvent(HotelId HotelId) : DomainEvent;