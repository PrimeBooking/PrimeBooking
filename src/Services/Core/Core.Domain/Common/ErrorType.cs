namespace Core.Domain.Common;

public static class ErrorType
{
    public const string NotFound = nameof(NotFound);
    public const string InvalidFormat = nameof(InvalidFormat);
    public const string NotSupported = nameof(NotSupported);
    public const string InternalServerError = nameof(InternalServerError);
}
