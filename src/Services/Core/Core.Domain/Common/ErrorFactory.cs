namespace Core.Domain.Common;

public static class ErrorFactory
{
    public static Error BuildError(string errorCode, string errorType, string detail, HttpStatusCode statusCode) =>
        new (errorCode, errorType, detail, null, statusCode);

    public static Error BuildExceptionError(this Exception exception, string errorCode, string errorType, HttpStatusCode statusCode) =>
        new (errorCode, errorType, exception.Message, exception.StackTrace, statusCode);
}
