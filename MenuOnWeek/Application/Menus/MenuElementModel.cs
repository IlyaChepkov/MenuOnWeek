using MenuOnWeek.Domain;

namespace MenuOnWeek.Application.Menus;

public sealed class MenuElementModel
{
    public Guid RecipeId { get; set; }

    public int ServeCount { get; set; }

    public DayOfWeek Date {  get; set; }

    public Meal Meal { get; set; }
}
