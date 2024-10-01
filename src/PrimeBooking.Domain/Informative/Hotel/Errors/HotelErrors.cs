using System.Net;

namespace PrimeBooking.Domain.Informative.Hotel.Errors;

public static class HotelErrors
{
    public static readonly Error NotFound = ErrorFactory.BuildError(
        ErrorCode.Validation,
        ErrorType.NotFound,
        "Hotel was not found",
        HttpStatusCode.NotFound
    );

    public static readonly Error EmptyNameValue =  ErrorFactory.BuildError(
        ErrorCode.Validation,
        ErrorType.InvalidFormat,
        "Hotel Name was empty",
        HttpStatusCode.UnprocessableEntity
    );

    public static readonly Error LessZeroCapacityValue =  ErrorFactory.BuildError(
        ErrorCode.Validation,
        ErrorType.InvalidFormat,
        "Capacity can't be less or equal zero",
        HttpStatusCode.UnprocessableEntity
    );
}
