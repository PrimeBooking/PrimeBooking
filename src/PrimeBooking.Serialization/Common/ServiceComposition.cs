using Microsoft.Extensions.DependencyInjection;
using PrimeBooking.Serialization.EventsSerialization;
using PrimeBooking.Serialization.MetadataSerialization;

namespace PrimeBooking.Serialization.Common;

public static class ServiceComposition
{
    public static IServiceCollection AddSerialization(this IServiceCollection services)
    {
        services.AddSingleton<IEventSerializer, EventSerializer>();
        services.AddSingleton<IMetadataSerializer, MetadataSerializer>();
        
        return services;
    }
}
