namespace Domain;

/// <summary>
/// Сущность единицы измерения
/// </summary>
public sealed class Unit
{
    public Unit(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Название единицы измерения
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// ID едининицы измерения
    /// </summary>
    public int ID { get; set; }

    public Dictionary<Unit, double> Table { get; set; }

}
