using MenuOnWeek.Domain;

namespace MenuOnWeek.Application.Menus;

public sealed class MenuElementViewModel
{
    public Guid RecipeId { get; set; }

    public int ServeCount { get; set; }

    public DaysOfWeek? Date { get; set; }

    public Meal? Meal { get; set; }
}
