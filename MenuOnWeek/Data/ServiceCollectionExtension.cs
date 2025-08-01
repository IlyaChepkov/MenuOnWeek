using MenuOnWeek.Application.Ingredients;
using MenuOnWeek.Application.Menus;
using MenuOnWeek.Application.Recipes;
using MenuOnWeek.Data;
using MenuOnWeek.Data.Ingredients;
using MenuOnWeek.Data.Menu;
using MenuOnWeek.Data.Recipes;
using MenuOnWeek.Data.Units;
using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddData(this IServiceCollection services)
    {
        services.AddOptions<DataOptions>().BindConfiguration(DataOptions.SectionKey);

        services.AddTransient(typeof(DBContextOptionsFactory<>));

        services.AddDbContext<DataContext>((provider, builder)
            => provider.GetRequiredService<DBContextOptionsFactory<DataContext>>().SetupOptions(builder));

        services.AddTransient<IUnitRepository, UnitRepository>();
        services.AddTransient<IIngredientRepository, IngredientRepository>();
        services.AddTransient<IRecipeRepository, RecipeRepository>();
        services.AddTransient<IMenuRepository, MenuRepository>();

        services.AddTransient<IFileRepository, FileRepository>();

        services.AddTransient<IIngredientUnitsRepository, IngredientUnitsRepository>();
        services.AddTransient<IRecipeIngredientsRepository, RecipeIngredientsRepository>();
        services.AddTransient<IMenuRecipesRepository, MenuRecipesRepository>();



        return services;
    }
}
