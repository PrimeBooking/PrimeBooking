namespace Accommodation.Application.Mappers.Abstract;

public interface IResolvedEventMapper
{
    StreamEvent ToStreamEvent(ResolvedEvent resolvedEvent);
}
