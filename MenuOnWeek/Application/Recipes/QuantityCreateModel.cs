namespace MenuOnWeek.Application.Recipes;

/// <summary>
/// Команда обновления количества ингредиента в рецепте
/// </summary>
public sealed record UpdateQuantityCommand( int Count, Guid UnitId);
