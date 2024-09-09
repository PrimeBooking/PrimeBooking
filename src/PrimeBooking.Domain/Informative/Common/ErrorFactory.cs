namespace PrimeBooking.Domain.Informative.Common;

public static class ErrorFactory
{
    public static Error BuildError(string errorCode, string errorType, string detail) =>
        new (errorCode, errorType, detail, null);
    
    public static Error BuildExceptionError(this Exception exception, string errorCode, string errorType) =>
        new (errorCode, errorType, exception.Message, exception.StackTrace);
}