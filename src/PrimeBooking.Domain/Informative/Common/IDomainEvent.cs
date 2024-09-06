namespace PrimeBooking.Domain.Informative.Common;

public interface IDomainEvent : INotification
{
    DateTime Time { get; init; }
}