namespace PrimeBooking.Domain.Common;

public interface IDomainEvent : INotification
{
    public Metadata Metadata { get; init; }
};
