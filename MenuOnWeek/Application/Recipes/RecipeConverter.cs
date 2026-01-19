using MenuOnWeek.Domain.Recipes;

namespace MenuOnWeek.Application.Recipes;

internal static class RecipeConverter
{
    internal static RecipeView ConvertToRecipeViewModel(this Recipe recipe)
    {
        return new RecipeView(
        
            recipe.Id,
            recipe.Name,
            recipe.FileId,
            recipe.Price,
            recipe.Description,
            recipe.RecipeIngredients.Select(x => (x.IngredientId, new QuantityView( x.Count, x.UnitId ))).ToDictionary()
        );
    }
}
