using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain;

public sealed class Recipe
{

    public Recipe(string name, int id)
    {
        Name = name;
        Id = id;
        Ingredients = new Dictionary<Ingredient, Quantity>();
    }

    /// <summary>
    /// Id рецепта
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Имя рецепта
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Цена рецепта
    /// </summary>
    public int Price { get { 
            return Ingredients
                .Select(x => (int)Math.Round(x.Key.Price *  x.Key.Table[x.Value.Unit]) * x.Value.Count)
                .Sum(); } }

    /// <summary>
    /// Хранимые ингредиенты рецепта
    /// </summary>
    public Dictionary<int, Quantity> RawIngredients { get; set; } = new Dictionary<int, Quantity>();

    /// <summary>
    /// Ингредиенты рецепта
    /// </summary>
    [JsonIgnore]
    public Dictionary<Ingredient, Quantity>? Ingredients { get; set; } = new Dictionary<Ingredient, Quantity>();
}
