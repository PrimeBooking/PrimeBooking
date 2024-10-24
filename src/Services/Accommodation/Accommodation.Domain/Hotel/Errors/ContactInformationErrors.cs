namespace Accommodation.Domain.Hotel.Errors;

public static class ContactInformationErrors
{
    public static Error EmptyValue(string detail) =>  ErrorFactory.BuildError(
        ErrorCode.Validation,
        ErrorType.InvalidFormat,
        detail,
        HttpStatusCode.UnprocessableEntity
    );
}
