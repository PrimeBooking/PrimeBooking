namespace PrimeBooking.Serialization.MetadataSerialization;

public interface IMetadataSerializer
{
    byte[] SerializeMetadata(Metadata metadata);
    Metadata DeserializeMetadata(byte[] bytes);
}
