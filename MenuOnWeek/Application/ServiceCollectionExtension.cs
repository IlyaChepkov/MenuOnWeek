using Application.Ingredients;
using Application.Units;
using MenuOnWeek.Application.Menus;
using MenuOnWeek.Application.Recipes;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IMenuService, MenuService>();
        services.AddTransient<IRecipeService, RecipeService>();
        services.AddTransient<IIngredientService, IngredientService>();
        services.AddTransient<IUnitService, UnitService>();
        return services;
    }
}
