namespace PrimeBooking.Serialization.MetadataSerialization;

public class MetadataSerializer : BaseSerializer, IMetadataSerializer
{
    public byte[] SerializeMetadata(Metadata metadata) =>
        JsonSerializer.SerializeToUtf8Bytes(metadata, Options);

    public Metadata DeserializeMetadata(byte[] bytes)
    {
        throw new NotImplementedException();
    }
}
