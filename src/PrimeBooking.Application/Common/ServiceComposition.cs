namespace PrimeBooking.Application.Common;

public static class ServiceComposition
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSerialization();
        services.AddMappers();
        
        return services;
    }

    private static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddTransient<IEventDataMapper, EventDataMapper>();
        services.AddTransient<IResolvedEventMapper, ResolvedEventMapper>();

        return services;
    }
}
