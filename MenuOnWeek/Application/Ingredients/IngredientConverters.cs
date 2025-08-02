using Application.Units;
using Domain;
using Utils;

namespace Application.Ingredients;

internal static class IngredientConverters
{
    internal static IngredientViewCommand ConvertToIngredientViewModel(this Ingredient ingredient)
    {
        return new IngredientViewCommand()
        {
            Id = ingredient.Id,
            Name = ingredient.Name,
            Price = ingredient.Price,
            Table = ingredient.IngredientUnits
               .Select(x => (new UnitViewCommand( x.UnitId, x?.Unit?.Name ), x.Required().Coeficient))
               .ToDictionary(y => y.Item1, y => y.Item2),
            UnitId = ingredient.UnitId
        };
    }
}
