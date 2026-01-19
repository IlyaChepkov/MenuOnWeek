using MenuOnWeek.Domain.Menus;
using MenuOnWeek.Domain.Recipes;
using MenuOnWeek.Domain.Units;
using MenuOnWeek.Utils;

namespace MenuOnWeek.Domain.Ingredients;

/// <summary>
/// Единица измерения ингредиента
/// </summary>
public sealed class IngredientUnits
{
    public Ingredient? ingredient;
    public Unit? unit;
    public double coeficient;

    private IngredientUnits(Guid ingredientId, Guid unitId, double coeficient)
    {
        IngredientId = ingredientId;
        UnitId = unitId;
        this.coeficient = coeficient;
    }

    /// <summary>
    /// Ингредиент
    /// </summary>
    public Ingredient Ingredient
    {
        get => ingredient ?? throw new IncludeEntityException<IngredientUnits, Ingredient>(nameof(Ingredient));
        set => ingredient = value;
    }

    /// <summary>
    /// Идентификатор ингредиента
    /// </summary>
    public Guid IngredientId { get; set; }

    /// <summary>
    /// Единица измерения
    /// </summary>
    public Unit Unit {
        get => unit ?? throw new IncludeEntityException<IngredientUnits, Unit>(nameof(Unit));
        set => unit = value;
    }

    /// <summary>
    /// Идентификатор единицы измерения
    /// </summary>
    public Guid UnitId { get; set; }

    /// <summary>
    /// Коэффициент
    /// </summary>
    public double Coeficient
    {
        get => coeficient;
        set
        {
            ValidationException<double>.ThrowIf(x => x <= 0, value, "Коэффициент должен быть положительным");
            coeficient = value;
        }
    }

    public static IngredientUnits Create(Guid ingredientId, Guid unitId, double coeficient)
    {
        return new IngredientUnits(ingredientId, unitId, coeficient);
    }
}
