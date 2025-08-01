using Application.Units;
using Domain;
using Utils;

namespace Application.Ingredients;

internal static class IngredientConverters
{
    internal static IngredientViewModel ConvertToIngredientViewModel(this Ingredient ingredient)
    {
        return new IngredientViewModel()
        {
            Id = ingredient.Id,
            Name = ingredient.Name,
            Price = ingredient.Price,
            Table = ingredient.IngredientUnits
               .Select(x => (new UnitViewModel() { Id = x.UnitId, Name =  x?.Unit?.Name }, x.Required().Coeficient))
               .ToDictionary(y => y.Item1, y => y.Item2),
            UnitId = ingredient.UnitId
        };
    }
}
