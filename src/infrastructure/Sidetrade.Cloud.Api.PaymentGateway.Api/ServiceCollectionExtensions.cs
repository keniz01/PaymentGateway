public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiMappings(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Program));
        return services;
    }
}

