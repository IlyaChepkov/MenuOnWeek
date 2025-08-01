namespace MenuOnWeek.Contracts.Recipes;

/// <summary>
/// Рецепт
/// </summary>
public sealed class RecipeCreateRequest
{

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
