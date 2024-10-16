namespace PrimeBooking.Application.Models;

public record StreamEvent(Guid Id, object? Payload, Metadata Metadata, string ContentType, long Position, bool FromArchive = false);
