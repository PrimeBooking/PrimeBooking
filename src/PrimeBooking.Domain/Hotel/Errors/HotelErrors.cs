namespace PrimeBooking.Domain.Hotel.Errors;

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
    
    public static readonly Error EmptyGuidValue =  ErrorFactory.BuildError(
        ErrorCode.Validation,
        ErrorType.InvalidFormat,
        "Guid can't be empty",
        HttpStatusCode.UnprocessableEntity
    );
}
