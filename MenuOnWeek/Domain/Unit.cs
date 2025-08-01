using MenuOnWeek.Domain;

namespace Domain;

/// <summary>
/// Сущность единицы измерения
/// </summary>
public sealed class Unit : IEntityWithId
{
    private Unit(string name)
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
    public Guid Id { get; set; }

    public static Unit Create(string name)
    {
        var ingredient = new Unit(name);
        ingredient.Id = Guid.NewGuid();
        return ingredient;
    }
}
