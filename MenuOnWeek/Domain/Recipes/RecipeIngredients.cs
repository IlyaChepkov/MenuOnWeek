using System.Xml.Linq;
using MenuOnWeek.Domain.Ingredients;
using MenuOnWeek.Domain.Units;
using MenuOnWeek.Utils;

namespace MenuOnWeek.Domain.Recipes;

/// <summary>
/// Элемент рецепта. Ингредент и сколько его нужно в этом блюде
/// </summary>
public sealed class RecipeIngredients
{
    private Recipe? recipe;

    private Ingredient? ingredient;

    private Unit? unit;

    private int count;

    private RecipeIngredients(Guid recipeId, Guid ingredientId, Guid unitId, int count)
    {
        RecipeId = recipeId;
        IngredientId = ingredientId;
        UnitId = unitId;
        Count = count;
    }

    /// <summary>
    /// Рецепт
    /// </summary>
    public Recipe Recipe
    {
        get => recipe ?? throw new IncludeEntityException<RecipeIngredients, Recipe>(nameof(Recipe));
        set => recipe = value;
    }

    /// <summary>
    /// Id рецепта
    /// </summary>
    public Guid RecipeId { get; set; }

    /// <summary>
    /// Ингредиент
    /// </summary>
    public Ingredient Ingredient
    {
        get => ingredient ?? throw new IncludeEntityException<RecipeIngredients, Ingredient>(nameof(Ingredient));
        set => ingredient = value;
    }

    /// <summary>
    /// Id ингредиента
    /// </summary>
    public Guid IngredientId { get; set; }

    /// <summary>
    /// Единица измерения
    /// </summary>
    public Unit Unit
    {
        get => unit ?? throw new IncludeEntityException<RecipeIngredients, Unit>(nameof(unit));
        set => unit = value;
    }

    /// <summary>
    /// Id единицы измерения
    /// </summary>
    public Guid UnitId { get; set; }

    /// <summary>
    /// Количество ингредиента
    /// </summary>
    public int Count
    {
        get => count;
        set
        {
            ValidationException<int>.ThrowIf(x => x < 1, value, "Количество не может быть меньше 1");
            count = value;
        }
    }

    public static RecipeIngredients Create(Guid recipeId, Guid ingredientId, Guid unitId, int count)
    {
        ValidationException<int>.ThrowIf(x => x < 1, count, "Количество не может быть меньше 1");
        return new RecipeIngredients(recipeId, ingredientId, unitId, count);
    }
}
