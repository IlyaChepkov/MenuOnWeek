using Application.Units;

namespace Application.Ingredients;

public sealed record CreateIngredientCommand(string Name, int Price, Guid UnitId, Dictionary<UnitViewCommand, double> Table);
