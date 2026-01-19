using System.ComponentModel.DataAnnotations;
using MenuOnWeek.Domain;
using MenuOnWeek.Utils;

namespace MenuOnWeek.Domain.Units;

/// <summary>
/// Сущность единицы измерения
/// </summary>
public sealed class Unit : IEntityWithId
{
    private string name;

    private Unit(string name)
    {
        this.name = name;
    }

    /// <inheritdoc/>
    public Guid Id { get; set; }

    /// <summary>
    /// Название единицы измерения
    /// </summary>
    public string Name
    {
        get => name;
        set
        {
            ValidationException<string>.ThrowIf(x => String.IsNullOrWhiteSpace(x), value, "Название не может быть пустым");
            name = value;
        }
    }

    public static Unit Create(string name)
    {
        var ingredient = new Unit(name);
        ingredient.Id = Guid.NewGuid();
        return ingredient;
    }
}
