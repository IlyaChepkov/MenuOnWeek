using Application.Units;

namespace Application.Ingredients;

/// <summary>
/// Комманда для создания ингредиента
/// </summary>
public sealed record CreateIngredientCommand(string Name, int Price, Guid UnitId, Dictionary<UnitView, double> Table);
