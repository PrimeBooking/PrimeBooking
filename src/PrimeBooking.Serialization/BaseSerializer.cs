namespace PrimeBooking.Serialization;

public abstract class BaseSerializer
{
    public static readonly JsonSerializerOptions Options = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        Converters = { new DomainEventConverter(), new JsonStringEnumConverter() }
    };
}
