using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddData(this IServiceCollection services)
    {
        services.AddSingleton<IDataContext, DataContext>();
        services.AddOptions<DataOptions>();
        return services;
    }
}
