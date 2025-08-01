using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace MenuOnWeek.Domain;

public sealed class RecipeIngredients
{
    private Ingredient? ingredient;

    private RecipeIngredients(Guid recipeId, Guid ingredientId, Guid unitId, int count)
    {
        RecipeId = recipeId;
        IngredientId = ingredientId;
        UnitId = unitId;
        Count = count;
    }

    public Recipe? Recipe { get; set; }

    public Guid RecipeId {  get; set; }

    public Ingredient Ingredient
    {
        get => ingredient ?? throw new Exception();
        set => ingredient = value;
    }

    public Guid IngredientId { get; set; }

    public Unit? Unit { get; set; }

    public Guid UnitId { get; set; }

    public int Count { get; set; }

    public static RecipeIngredients Create(Guid recipeId, Guid ingredientId, Guid unitId, int count)
    {
        return new RecipeIngredients( recipeId, ingredientId, unitId, count);
    }
}
