using Application;
using Data;
using MenuOnWeek.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace MenuOnWeek.Frontend;

internal static class Startup
{
    internal static IServiceProvider Load(IServiceCollection service)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json");

        service.AddSingleton<IConfiguration>(configuration.Build());

        service.AddData();
        service.AddApplication();
        service.AddClients();
        return service.BuildServiceProvider();
    }
}
