namespace Accommodation.Application.Common;

public static class ServiceComposition
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSerialization();
        services.AddMappers();
        services.AddMediatr();
        
        return services;
    }

    private static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddTransient<IEventDataMapper, EventDataMapper>();
        services.AddTransient<IResolvedEventMapper, ResolvedEventMapper>();

        return services;
    }

    private static IServiceCollection AddMediatr(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        
        services.AddMediatR(configuration 
            => configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}
