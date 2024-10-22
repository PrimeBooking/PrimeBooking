namespace Accommodation.Infrastructure.Common;

public static class ServiceComposition
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        AddEventStore(services, configuration);

        AddRepository(services);
        
        AddRabbitMq(services, configuration);
        
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

    private static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(busRegistrationConfig => 
        {
            //configuration.AddConsumer<BasketCheckoutConsumer>();
    
            busRegistrationConfig.UsingRabbitMq((context, configurator) => {
                
                configurator.Host(configuration["EventBusSettings:HostAddress"]);
        
                configurator.ReceiveEndpoint(Constants.HotelDataTransferQueue, c => {
                    //c.ConfigureConsumer<BasketCheckoutConsumer>(context);
                });
            });
        });

        return services;
    }
}
