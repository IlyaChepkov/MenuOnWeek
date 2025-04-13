using System.Text.Json.Serialization;

namespace Domain;

public sealed class Quantity
{
    public Quantity(int count, Unit unit)
    {
        Count = count;
        Unit = unit;
    }

    /// <summary>
    /// Количество
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Id единицы измерения
    /// </summary>
    public int UnitId { get; set; }

    /// <summary>
    /// Единицы измерения
    /// </summary>
    [JsonIgnore]
    public Unit? Unit { get; set; }

}
