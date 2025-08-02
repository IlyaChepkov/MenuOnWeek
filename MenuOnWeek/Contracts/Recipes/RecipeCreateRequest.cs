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
    /// Картинка рецепта
    /// </summary>
    public string? Image {  get; set; }

    /// <summary>
    /// Ингредиенты рецепта
    /// </summary>
    public IReadOnlyList<RecipeIngredientsCreateOrUpdateRequest> Ingredients { get; set; } = Array.Empty<RecipeIngredientsCreateOrUpdateRequest>();
}
