using MenuOnWeek.Domain;
using MenuOnWeek.Domain.Menus;

namespace MenuOnWeek.Application.Menus;

/// <summary>
/// Модель для просмотра элементов меню
/// </summary>
public sealed record MenuElementView
(
    Guid RecipeId,

    int ServeCount,

    DaysOfWeek? Date,

    Meal? Meal
);
