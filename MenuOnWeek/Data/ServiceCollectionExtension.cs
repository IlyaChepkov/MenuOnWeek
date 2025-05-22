using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddData(this IServiceCollection services)
    {
        services.AddSingleton<DataContext>();
        services.AddOptions<DataOptions>().BindConfiguration(DataOptions.SectionKey);
        services.AddTransient<IUnitRepository, UnitRepository>();
        services.AddTransient<IIngredientRepository, IngredientRepository>();
        services.AddTransient<IRecipeRepository, RecipeRepository>();
        services.AddTransient<IMenuRepository, MenuRepository>();
        return services;
    }
}
