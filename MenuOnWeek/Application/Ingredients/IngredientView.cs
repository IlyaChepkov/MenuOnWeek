using MenuOnWeek.Domain;
using Application.Units;

namespace Application.Ingredients;

/// <summary>
/// Модель для просмотра ингредиента
/// </summary>
public sealed record IngredientView(
    Guid Id,
    string Name,
    int Price,
    Guid UnitId,
    IReadOnlyDictionary<UnitView, double> IngredientUnits
);
