using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using MenuOnWeek.Domain;

namespace Domain;

public sealed class MenuRecipes
{
    private MenuRecipes(Guid id, Guid menuId, Guid recipeId, int serve, DaysOfWeek? date, Meal? meal)
    {
        Id = id;
        MenuId = menuId;
        RecipeId = recipeId;
        Serve = serve;
        Date = date;
        Meal = meal;
    }

    public Guid Id { get; set; }

    public Menu? Menu { get; set; }

    public Guid MenuId { get; set; }

    public Recipe? Recipe { get; set; }

    public Guid RecipeId { get; set; }

    public int Serve {  get; set; }

    public DaysOfWeek? Date { get; set; }

    public Meal? Meal { get; set; }

    public static MenuRecipes Create(Guid menuId, Recipe recipe, int serve, DaysOfWeek? day, Meal? meal)
    {
        var menuRecipes = new MenuRecipes(Guid.NewGuid(), menuId, recipe.Id, serve, day, meal);
        menuRecipes.Recipe = recipe;
        return menuRecipes;
    }

}
