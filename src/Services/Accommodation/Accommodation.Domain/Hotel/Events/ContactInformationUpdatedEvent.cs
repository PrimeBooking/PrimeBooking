using Accommodation.Domain.Hotel;

namespace PrimeBooking.Domain.Hotel.Events;

public record ContactInformationUpdatedEvent(ContactInformation ContactInformation) : DomainEvent;