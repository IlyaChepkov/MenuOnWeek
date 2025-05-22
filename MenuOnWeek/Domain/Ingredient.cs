using System.Text.Json.Serialization;

namespace Domain;

public sealed class Ingredient
{
    public Ingredient(string name, int price, Unit unit, Guid unitId)
    {
        Name = name;
        Price = price;
        Unit = unit;
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
    [JsonIgnore]
    public Unit? Unit {  get; set; }

    /// <summary>
    /// Хранимая таблица переводов единиц измерения
    /// </summary>
    public Dictionary<Guid, double> RawTable { get; set; } = new Dictionary<Guid, double>();

    /// <summary>
    /// Таблица переводов единиц измерения
    /// </summary>
    [JsonIgnore]
    public Dictionary<Unit, double> Table { get; set; } = new Dictionary<Unit, double>();

    public static Ingredient Create(string name, int price, Unit unit, Dictionary<Unit, double> table)
    {
        var ingredient = new Ingredient(name, price, unit, unit.Id);
        ingredient.Id = Guid.NewGuid();
        ingredient.Table = table;
        return ingredient;
    }
}
