namespace PrimeBooking.Serialization.Converters;

public class DomainEventConverter : JsonConverter<IDomainEvent>
{
    public override IDomainEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, IDomainEvent value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}
