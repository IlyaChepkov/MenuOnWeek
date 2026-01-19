using MenuOnWeek.Domain.Recipes;
using MenuOnWeek.Utils;

namespace MenuOnWeek.Domain.Menus;

/// <summary>
/// Элемент меню. Рецепт, прием пищи день недели и количество порций
/// </summary>
public sealed class MenuRecipes
{
    private Menu? menu;
    private Recipe? recipe;
    private int serve;

    private MenuRecipes(Guid id, Guid menuId, Guid recipeId, int serve, DaysOfWeek? date, Meal? meal)
    {
        Id = id;
        MenuId = menuId;
        RecipeId = recipeId;
        this.serve = serve;
        Date = date;
        Meal = meal;
    }

    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Меню
    /// </summary>
    public Menu Menu
    {
        get => menu ?? throw new IncludeEntityException<MenuRecipes, Menu>(nameof(Menu));
        set => menu = value;
    }

    /// <summary>
    /// Идентификатор меню
    /// </summary>
    public Guid MenuId { get; set; }

    /// <summary>
    /// Рецепт
    /// </summary>
    public Recipe Recipe
    {
        get => recipe ?? throw new IncludeEntityException<MenuRecipes, Recipe>(nameof(Recipe));
        set => recipe = value;
    }

    /// <summary>
    /// Идентификатор рецепта
    /// </summary>
    public Guid RecipeId { get; set; }

    /// <summary>
    /// количество порций
    /// </summary>
    public int Serve
    {
        get => serve;
        set
        {
            ValidationException<int>.ThrowIf(x => x < 1, value, "Количество порций не может быть шеньше 1");
        }
    }

    /// <summary>
    /// День недели
    /// </summary>
    public DaysOfWeek? Date { get; set; }

    /// <summary>
    /// Прием пищи
    /// </summary>
    public Meal? Meal { get; set; }

    public static MenuRecipes Create(Guid menuId, Recipe recipe, int serve, DaysOfWeek? day, Meal? meal)
    {
        ValidationException<int>.ThrowIf(x => x < 1, serve, "Количество порций не может быть шеньше 1");
        var menuRecipes = new MenuRecipes(Guid.NewGuid(), menuId, recipe.Id, serve, day, meal);
        menuRecipes.Recipe = recipe;
        return menuRecipes;
    }

}
