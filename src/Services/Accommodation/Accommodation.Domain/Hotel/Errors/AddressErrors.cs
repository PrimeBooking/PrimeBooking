namespace Accommodation.Domain.Hotel.Errors;

public static class AddressErrors
{
    public static Error EmptyValue(string detail) =>  ErrorFactory.BuildError(
        ErrorCode.Validation,
        ErrorType.InvalidFormat,
        detail,
        HttpStatusCode.UnprocessableEntity
    );
}
