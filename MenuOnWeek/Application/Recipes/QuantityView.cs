namespace MenuOnWeek.Application.Recipes;


/// <summary>
/// модель для просмотра количества ингредиента в рецепте
/// </summary>
public sealed record QuantityView(double Count, Guid UnitId);
