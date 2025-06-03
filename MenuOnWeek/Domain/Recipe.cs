using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Utils;

namespace Domain;

public sealed class Recipe
{

    public Recipe(string name, Guid? image, string description)
    {
        Name = name;
        Image = image;
        Description = description;
    }

    /// <summary>
    /// Id рецепта
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Имя рецепта
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Цена рецепта
    /// </summary>
    public int Price { get { 
            return Ingredients
                .Select(x => (int)Math.Round(x.Key.Price *  x.Key.Table[x.Value.Unit.Required()]) * x.Value.Count)
                .Sum(); } }

    public string Description { get; set; }

    public Guid? Image { get; set; }

    /// <summary>
    /// Хранимые ингредиенты рецепта
    /// </summary>
    public Dictionary<Guid, Quantity> RawIngredients { get; set; } = new Dictionary<Guid, Quantity>();

    /// <summary>
    /// Ингредиенты рецепта
    /// </summary>
    [JsonIgnore]
    public Dictionary<Ingredient, Quantity> Ingredients { get; set; } = new Dictionary<Ingredient, Quantity>();

    public static Recipe Create(string name, Guid? image, string description)
    {
        var ingredient = new Recipe(name, image, description);
        ingredient.Id = Guid.NewGuid();
        return ingredient;
    }
}
