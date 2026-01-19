using Application.Units;

namespace Application.Ingredients;

/// <summary>
/// Комманда обновления ингредиента
/// </summary>
public sealed record UpdateIngredientCommand
(
    Guid Id,
    string Name,
    int Price,
    Guid UnitId,
    IReadOnlyDictionary<UnitView, double> Table
);
