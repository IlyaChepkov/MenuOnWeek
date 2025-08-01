namespace MenuOnWeek.Contracts.Units;

/// <summary>
/// Единица измерения
/// </summary>
public sealed class UnitResponse
{
    /// <summary>
    /// Id единицы измерения
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// Название единицы измерения
    /// </summary>
    public string? Name { get; set; }
}
