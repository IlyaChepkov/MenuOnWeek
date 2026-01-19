namespace MenuOnWeek.Application.Recipes;

/// <summary>
/// Комманда создания рецепта
/// </summary>
public sealed record RecipeCreateCommand(
    string Name,
    Guid? FileId,
    string Description,
    IReadOnlyDictionary<Guid, UpdateQuantityCommand> Ingredients
);
