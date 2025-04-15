using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddData(this IServiceCollection services)
    {
        services.AddSingleton<DataContext>();
        services.AddOptions<DataOptions>();
        return services;
    }
}
