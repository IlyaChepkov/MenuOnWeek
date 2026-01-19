using MenuOnWeek.Domain;
using MenuOnWeek.Domain.Menus;

namespace MenuOnWeek.Frontend.Menu;

internal sealed record MenuElementDto(
    Guid RecipeId,
    int ServeCount,
    DaysOfWeek? Date,
    Meal? Meal );
