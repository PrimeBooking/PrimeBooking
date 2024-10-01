namespace PrimeBooking.Domain.Common;

public abstract class Entity<TEntityId>
{
    public TEntityId Id { get; init; }    
    
    protected Entity(TEntityId id)
    {
        Id = id;
    }

    protected Entity()
    {
    }
}