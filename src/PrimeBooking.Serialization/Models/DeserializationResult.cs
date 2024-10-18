namespace PrimeBooking.Serialization.Models;

public record DeserializationResult;

public record SuccessfulDeserialization(object Payload) : DeserializationResult;
public record FailedToDeserialize(Error Error) : DeserializationResult;
