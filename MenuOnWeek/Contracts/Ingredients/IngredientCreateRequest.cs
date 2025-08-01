namespace MenuOnWeek.Contracts.Ingredients;

/// <summary>
/// Ингредиент
/// </summary>
public sealed class IngredientCreateRequest
{
    /// <summary>
    /// Название ингредиента
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Id единицы измерения
    /// </summary>
    public Guid? UnitId { get; set; }

    /// <summary>
    /// Таблица единиц измерения
    /// </summary>
    public IReadOnlyList<IngredientUnitsCreateOrUpdateRequest> Units { get; set; } = Array.Empty<IngredientUnitsCreateOrUpdateRequest>();
}
