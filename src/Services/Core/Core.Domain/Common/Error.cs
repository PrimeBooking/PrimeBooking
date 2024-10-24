namespace Core.Domain.Common;

public record Error(string ErrorCode, string ErrorType, string Detail, string? StackTrace, HttpStatusCode StatusCode);
