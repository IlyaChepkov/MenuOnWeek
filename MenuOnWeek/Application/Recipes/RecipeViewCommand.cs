namespace MenuOnWeek.Application.Recipes;

/// <summary>
/// Модель для просмотра рецепта
/// </summary>
public sealed record RecipeView(
    Guid Id,
    string Name,
    Guid? FileId,
    double Price,
    string Description,
    Dictionary<Guid, QuantityView> Ingredients
);
