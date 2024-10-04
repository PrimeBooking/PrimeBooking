namespace PrimeBooking.Domain.Tests.Common;

public class AggregateRootTests
{
    private static readonly DummyStructuredId Id = new(Guid.NewGuid());
    private static readonly DummyDomainEvent DummyDomainEvent = new();
    private static readonly DummyAggregateRoot DummyAggregate = new(Id);

    [Fact]
    public void Aggregates_WithSameId_ShouldReturnEqualsFalse()
    {
        var aggregateId = new DummyStructuredId(Guid.NewGuid());

        var aggregateRoot1 = new DummyAggregateRoot(aggregateId);
        var aggregateRoot2 = new DummyAggregateRoot(aggregateId);

        (aggregateRoot1.GetHashCode() == aggregateRoot2.GetHashCode()).Should().BeFalse();
        aggregateRoot1.Equals(aggregateRoot2).Should().BeFalse();
    }
    
    [Fact]
    public void RaiseDomainEvent_ShouldAddDomainEvent()
    {
        DummyAggregate.RaiseDomainEvent(DummyDomainEvent);
    }

    [Fact]
    public void GetDomainEvents_ShouldReturnListOfDomainEvents()
    {
        DummyAggregate.GetDomainEvents().Should().BeEquivalentTo(new[] { DummyDomainEvent });
    }

    
    [Fact]
    public void ClearDomainEvents_ShouldRemoveAllDomainEvents()
    {
        DummyAggregate.ClearDomainEvents();
        DummyAggregate.GetDomainEvents().Should().BeEmpty();
    }
}