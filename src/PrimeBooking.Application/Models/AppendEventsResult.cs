namespace PrimeBooking.Application.Models;

public record AppendEventsResult(ulong CommitPosition, long NextExpectedStreamRevision);
