using Application.Units;

namespace MenuOnWeek.Frontend.Ingredient;

internal sealed record IngredientDto(
    string Name,
    int Price,
    Guid UnitId,
    Dictionary<Guid, double> Table);
