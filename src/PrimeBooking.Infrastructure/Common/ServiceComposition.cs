using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrimeBooking.Domain.Hotel;
using PrimeBooking.Infrastructure.EventStore;
using PrimeBooking.Infrastructure.EventStore.Repositories;

namespace PrimeBooking.Infrastructure.Common;

public static class ServiceComposition
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        AddEventStore(services, configuration);

        AddRepository(services);
        
        return services;
    }

    private static IServiceCollection AddEventStore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(_ => {
            var settings = EventStoreClientSettings.Create(
                configuration.GetConnectionString("EventStoreConnection") 
                ?? throw new InvalidOperationException("EventStore connection string is missing."));
            return new EventStoreClient(settings);
        });

        return services;
    }

    private static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddTransient<IEventStoreRepository<Hotel>, EventStoreRepository<Hotel>>();

        return services;
    }
}
