using PrimeBooking.Domain.Informative.Common;

namespace PrimeBooking.Domain.Informative.Hotel.Errors;

public static class HotelErrors
{
    public static readonly Error NotFound =  new(ErrorCode.Validation, ErrorType.NotFound, "Hotel was not found");
    public static readonly Error EmptyNameValue =  new(ErrorCode.Validation, ErrorType.InvalidFormat, "Hotel Name was empty");
}