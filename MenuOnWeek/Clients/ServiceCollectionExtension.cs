using MenuOnWeek.Clients.Files;
using MenuOnWeek.Clients.Ingredients;
using MenuOnWeek.Clients.Menus;
using MenuOnWeek.Clients.Recipes;
using MenuOnWeek.Clients.Units;
using Microsoft.Extensions.DependencyInjection;

namespace MenuOnWeek.Clients;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddClients(this IServiceCollection services)
    {
        services.AddHttpClient<IMenuClient, MenuClient>();
        services.AddHttpClient<IRecipeClient, RecipeClient>();
        services.AddHttpClient<IIngredientClient, IngredientClient>();
        services.AddHttpClient<IUnitClient, UnitClient>();
        services.AddHttpClient<IFileClient, FileClient>();

        services.AddOptions<ServerConnectionOptions>().BindConfiguration(ServerConnectionOptions.SectionKey);

        return services;
    }
}
