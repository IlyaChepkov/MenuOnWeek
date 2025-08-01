namespace MenuOnWeek.Contracts.Ingredients;

/// <summary>
/// Единица измерения использующаяся в ингредиенте
/// </summary>
public sealed class IngredientUnitsResponse
{
    /// <summary>
    /// Id единицы измерения
    /// </summary>
    public Guid? UnitId { get; set; }

    /// <summary>
    /// Коэффициент
    /// </summary>
    public double? Coeficient {  get; set; }
}
