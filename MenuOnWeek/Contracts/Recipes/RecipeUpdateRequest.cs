namespace MenuOnWeek.Contracts.Recipes;

/// <summary>
/// Рецепт
/// </summary>
public sealed class RecipeUpdateRequest
{
    /// <summary>
    /// Id рецепта
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// Название рецепта
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Описание рецепта
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Ингредиенты рецепта
    /// </summary>
    public List<RecipeIngredientsCreateOrUpdateRequest> Ingredients { get; set; } = new List<RecipeIngredientsCreateOrUpdateRequest>();
}
