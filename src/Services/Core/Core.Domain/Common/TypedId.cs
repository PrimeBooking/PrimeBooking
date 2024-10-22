namespace Core.Domain.Common;

public record TypedId<T>
{
    public T Value { get; }

    public TypedId(T value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        Value = value;
    }
};
