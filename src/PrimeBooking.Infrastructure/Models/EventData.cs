namespace PrimeBooking.Infrastructure.Models;

public sealed record EventData
{
    public Guid EventId { get; init; }
    public string Type { get; init; }
    public byte[] Data { get; init; }
    public byte[] Metadata { get; init; }
};