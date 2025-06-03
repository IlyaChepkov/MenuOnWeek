using Application.Ingredients;
using MenuOnWeek.Application.Recipes;
using Microsoft.Extensions.DependencyInjection;

namespace MenuOnWeek.Frontend.Recipe;

public sealed partial class RecipeControl : UserControl
{
    private readonly IRecipeService recipeService;

    public RecipeControl()
    {
        recipeService = Program.ServiceProvider.GetRequiredService<IRecipeService>();

        InitializeComponent();

        RefreshRecipesList();
    }

    private void RefreshRecipesList()
    {
        var recipes = recipeService.GetAll(0, 100).ToArray();
        RecipesList.Items.Clear();
        RecipesList.Items.AddRange(recipes);
    }
}
