namespace MenuOnWeek.Application.Recipes;

/// <summary>
/// Команда обновления рецепта
/// </summary>
public sealed record RecipeUpdateCommand
(
    Guid Id,
    string Name,
    Guid? FileId,
    string Description,
    Dictionary<Guid, UpdateQuantityCommand> Ingredients
);
