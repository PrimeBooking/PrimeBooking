namespace PrimeBooking.Domain.Tests.DummyData;

public sealed record DummyStructuredId(Guid Value) : TypedId<Guid>(Value);
