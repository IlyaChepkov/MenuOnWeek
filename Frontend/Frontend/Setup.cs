using Microsoft.Extensions.DependencyInjection;
using Data;
using Application;

namespace Frontend;

internal static class Setup
{
    internal static IServiceProvider Load(IServiceCollection service)
    {
        service.AddData();
        service.AddApplication();
        return service.BuildServiceProvider();
    }
}
