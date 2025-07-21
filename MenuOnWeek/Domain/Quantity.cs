using System.Text.Json.Serialization;

namespace Domain;

public sealed class Quantity
{
    public Quantity(double count, Unit unit)
    {
        Count = count;
        Unit = unit;
    }

    /// <summary>
    /// Количество
    /// </summary>
    public double Count { get; set; }

    /// <summary>
    /// Id единицы измерения
    /// </summary>
    public Guid UnitId { get; set; }

    /// <summary>
    /// Единицы измерения
    /// </summary>
    [JsonIgnore]
    public Unit? Unit { get; set; }

}
