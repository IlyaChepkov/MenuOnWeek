using System.ComponentModel.DataAnnotations.Schema;
using MenuOnWeek.Utils;
using Utils;

namespace MenuOnWeek.Domain.Recipes;

public sealed class Recipe : IEntityWithId
{
    private List<RecipeIngredients> recipeIngredients = [];
    private string name;

    public Recipe(string name, Guid? fileId, string description)
    {
        this.name = name;
        FileId = fileId;
        Description = description;
    }

    /// <inheritdoc/>
    public Guid Id { get; set; }

    /// <summary>
    /// Имя рецепта
    /// </summary>
    public string Name {
        get => name;
        set
        {
            ValidationException<string>.ThrowIf(x => String.IsNullOrWhiteSpace(x), value, "Название не может быть пустым");
            name = value;
        }
    }

    /// <summary>
    /// Цена рецепта#
    public double Price
    {
        get
        {
            return recipeIngredients
                .Select(x => Math.Round((x.Ingredient.Required().UnitId == x.UnitId ? x.Ingredient.Required().Price : x.Ingredient.Required().IngredientUnits.Single(y => y.UnitId == x.UnitId).Coeficient * x.Ingredient.Required().Price) * x.Count))
                .Sum();
        }
    }

    public string Description { get; set; }

    public Guid? FileId { get; set; }

    /// <summary>
    /// Ингредиенты рецепта
    /// </summary>
    [NotMapped]
    public IReadOnlyList<RecipeIngredients> RecipeIngredients => recipeIngredients;

    public static Recipe Create(string name, Guid? fileId, string description)
    {
        ValidationException<string>.ThrowIf(x => String.IsNullOrWhiteSpace(x), name, "Название не может быть пустым");
        var ingredient = new Recipe(name, fileId, description);
        ingredient.Id = Guid.NewGuid();
        return ingredient;
    }
}
