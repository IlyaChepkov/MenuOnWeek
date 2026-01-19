using Application.Units;
using MenuOnWeek.Domain.Ingredients;
using Utils;

namespace Application.Ingredients;

internal static class IngredientConverters
{
    internal static IngredientView ConvertToIngredientViewModel(this Ingredient ingredient)
    {
        return new IngredientView(
        
            ingredient.Id,
            ingredient.Name,
            ingredient.Price,
            ingredient.UnitId,
            ingredient.IngredientUnits
               .Select(x => (new UnitView(x.UnitId, x?.Unit?.Name), x.Required().Coeficient))
               .ToDictionary(y => y.Item1, y => y.Item2)
        );
    }
}
