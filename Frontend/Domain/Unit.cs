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
    /// Id едининицы измерения
    /// </summary>
    public int Id { get; set; }
}
