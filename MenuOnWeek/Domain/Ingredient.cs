// Ignore Spelling: name Table Create

using MenuOnWeek.Domain;

namespace Domain;

public sealed class Ingredient : IEntityWithId
{
    private List<IngredientUnits> ingredientUnits = [];

    private Ingredient(string name, int price, Guid unitId)
    {
        Name = name;
        Price = price;
        UnitId = unitId;
    }

    /// <summary>
    /// Id ингредиента
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название ингредиента
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Цена ингредиента 
    /// </summary>
    public int Price { get; set; }

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
