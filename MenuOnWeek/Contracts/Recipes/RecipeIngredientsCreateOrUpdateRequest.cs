namespace MenuOnWeek.Contracts.Recipes;

/// <summary>
/// Ингредиент использующийся в рецепте
/// </summary>
public sealed class RecipeIngredientsCreateOrUpdateRequest
{
    /// <summary>
    /// Id ингредиента
    /// </summary>
    public Guid? IngredientId { get; set; }

    /// <summary>
    /// Id единицы измерения
    /// </summary>
    public Guid? UnitId { get; set; }

    /// <summary>
    /// Количество ингредиента
    /// </summary>
    public int? Count { get; set; }
}
