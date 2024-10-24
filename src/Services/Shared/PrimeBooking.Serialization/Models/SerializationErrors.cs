using System.Net;

namespace PrimeBooking.Serialization.Models;

public static class SerializationErrors
{
    public static Error FailedDeserialization(string detail) =>  ErrorFactory.BuildError(
        ErrorCode.Validation,
        ErrorType.InvalidFormat,
        detail,
        HttpStatusCode.UnprocessableEntity
    );
}
