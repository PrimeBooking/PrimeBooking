using System.Net;

namespace PrimeBooking.Serialization.MetadataSerialization;

public class MetadataSerializer : BaseSerializer, IMetadataSerializer
{
    public byte[] SerializeMetadata(Metadata metadata) =>
        JsonSerializer.SerializeToUtf8Bytes(metadata, Options);

    public Metadata DeserializeMetadata(ReadOnlySpan<byte> bytes)
    {
        try 
        {
            return bytes.Length == 0 ? null : JsonSerializer.Deserialize<Metadata>(bytes, Options);
        } 
        catch (Exception ex) 
        {
            //logging
            Error error = ex.BuildExceptionError(ErrorCode.UnhandledRequest, ErrorType.InternalServerError,
                HttpStatusCode.InternalServerError);
            
            return null;
        }
    }
}
