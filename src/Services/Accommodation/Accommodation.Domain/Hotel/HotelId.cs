namespace Accommodation.Domain.Hotel;

public record HotelId(Guid Id) : TypedId<Guid>(Id);
