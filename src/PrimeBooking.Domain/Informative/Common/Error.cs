using System.Net;

namespace PrimeBooking.Domain.Informative.Common;

public record Error(string ErrorCode, string ErrorType, string Detail, string? StackTrace, HttpStatusCode StatusCode);
