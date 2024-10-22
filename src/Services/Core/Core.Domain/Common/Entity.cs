namespace Core.Domain.Common;

public abstract class Entity<TEntityId>
{
    public TEntityId Id { get; protected init; } 
    
    protected Entity(TEntityId id)
    {
        Id = id;
    }

    protected Entity()
    {
    }
}
