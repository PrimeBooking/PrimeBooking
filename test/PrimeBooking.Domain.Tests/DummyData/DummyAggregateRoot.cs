namespace PrimeBooking.Domain.Tests.DummyData;

public class DummyAggregateRoot : AggregateRoot<DummyStructuredId>
{
    public DummyAggregateRoot(DummyStructuredId id) 
        => Id = id;
}