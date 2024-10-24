namespace Core.Domain.Common;

public record Metadata
{
    public Guid CorrelationId { get; init; } = Guid.NewGuid();
    public Guid CausationId { get; init; } = Guid.NewGuid();
};
