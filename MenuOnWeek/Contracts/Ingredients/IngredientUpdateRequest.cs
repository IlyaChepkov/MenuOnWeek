namespace MenuOnWeek.Contracts.Ingredients;

/// <summary>
/// Ингредиент
/// </summary>
public sealed class IngredientUpdateRequest
{
    /// <summary>
    /// Id ингредиента
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// Название ингредиента
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Id единицы измерения
    /// </summary>
    public Guid? UnitId { get; set; }

    /// <summary>
    /// Цена ингредиента
    /// </summary>
    public int? Price { get; set; }

    /// <summary>
    /// Таблица единиц измерения
    /// </summary>
    public IReadOnlyList<IngredientUnitsCreateOrUpdateRequest> Units { get; set; } =  Array.Empty<IngredientUnitsCreateOrUpdateRequest>();
}
