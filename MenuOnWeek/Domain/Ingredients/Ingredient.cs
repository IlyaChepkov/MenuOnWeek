using MenuOnWeek.Domain.Units;
using MenuOnWeek.Utils;

namespace MenuOnWeek.Domain.Ingredients;

/// <summary>
/// Ингредиент
/// </summary>
public sealed class Ingredient : IEntityWithId
{
    private List<IngredientUnits> ingredientUnits = [];
    private string name;
    private int price;

    private Ingredient(string name, int price, Guid unitId)
    {
        this.name = name;
        Name = name;
        this.price = price;
        Price = price;
        UnitId = unitId;
    }

    /// <inheritdoc/>
    public Guid Id { get; set; }

    /// <summary>
    /// Название ингредиента
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

    /// <summary>
    /// Цена ингредиента 
    /// </summary>
    public int Price
    {
        get => price;
        set
        {
            ValidationException<int>.ThrowIf(x => x <= 0, value, "Цена должна быть положительной");
            price = value;
        }
    }

    /// <summary>
    /// Идентификатор единицы измерения
    /// </summary>
    public Guid UnitId { get; set; }

    /// <summary>
    /// Единица измерения ингредиента
    /// </summary>
    public Unit? Unit { get; set; }

    /// <summary>
    /// Таблица переводов единиц измерения
    /// </summary>
    public IReadOnlyList<IngredientUnits> IngredientUnits => ingredientUnits;

    public static Ingredient Create(string name, int price, Unit unit)
    {
        var ingredient = new Ingredient(name, price, unit.Id);
        ingredient.Id = Guid.NewGuid();
        return ingredient;
    }
}
