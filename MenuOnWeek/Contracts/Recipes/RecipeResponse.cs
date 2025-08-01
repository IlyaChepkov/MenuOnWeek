namespace MenuOnWeek.Contracts.Recipes;

/// <summary>
/// Рецепт
/// </summary>
public sealed class RecipeResponse
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
    /// Цена рецепта
    /// </summary>
    public int? Price { get; set; }

    /// <summary>
    /// Ингредиенты рецепта
    /// </summary>
    public IReadOnlyList<RecipeIngredientsResponse> Ingredients { get; set; } = Array.Empty<RecipeIngredientsResponse>();
}
