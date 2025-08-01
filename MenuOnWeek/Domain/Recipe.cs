using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MenuOnWeek.Domain;
using Utils;

namespace Domain;

public sealed class Recipe : IEntityWithId
{
    private List<RecipeIngredients> recipeIngredients = [];

    public Recipe(string name, string? image, string description)
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
    /// Цена рецепта#
    public double Price { get
        {
            return recipeIngredients
                .Select(x => Math.Round((x.Ingredient.Required().UnitId == x.UnitId ? x.Ingredient.Required().Price : x.Ingredient.Required().IngredientUnits.Single(y => y.UnitId == x.UnitId).Coeficient * x.Ingredient.Required().Price) * x.Count))
                .Sum();
        } }

    public string Description { get; set; }

    public string? Image { get; set; }

    /// <summary>
    /// Ингредиенты рецепта
    /// </summary>
    [NotMapped]
    public IReadOnlyList<RecipeIngredients> RecipeIngredients => recipeIngredients;

    public static Recipe Create(string name, string? image, string description)
    {
        var ingredient = new Recipe(name, image, description);
        ingredient.Id = Guid.NewGuid();
        return ingredient;
    }
}
