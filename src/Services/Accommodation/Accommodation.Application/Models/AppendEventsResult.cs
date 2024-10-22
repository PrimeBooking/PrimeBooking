namespace Accommodation.Application.Models;

public record AppendEventsResult(ulong CommitPosition, long NextExpectedStreamRevision);
