using System.Text.Json.Serialization;

namespace Domain;

public sealed class Ingredient
{
    public Ingredient(string name, int price, int id, Unit unit)
    {
        Name = name;
        Price = price;
        Id = id;
        Unit = unit;
    }

    /// <summary>
    /// Id ингредиента
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название ингредиента
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Цена ингредиента 
    /// </summary>
    public int Price { get; set; }

    public int UnitId { get; set; }

    /// <summary>
    /// Единица измерения ингредиента
    /// </summary>
    [JsonIgnore]
    public Unit? Unit {  get; set; }

    /// <summary>
    /// Хранимая таблица переводов единиц измерения
    /// </summary>
    public Dictionary<int, double> RawTable { get; set; } = new Dictionary<int, double>();

    /// <summary>
    /// Таблица переводов единиц измерения
    /// </summary>
    [JsonIgnore]
    public Dictionary<Unit, double>? Table { get; set; } = new Dictionary<Unit, double>();
}
