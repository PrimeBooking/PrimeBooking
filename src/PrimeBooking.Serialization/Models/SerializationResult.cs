namespace PrimeBooking.Serialization.Models;

public record SerializationResult(string EventType, string ContentType, byte[] Payload);
