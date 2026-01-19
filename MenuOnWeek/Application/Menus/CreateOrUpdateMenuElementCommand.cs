using MenuOnWeek.Domain;
using MenuOnWeek.Domain.Menus;

namespace MenuOnWeek.Application.Menus;

/// <summary>
/// Комманда для создания или обновления элемента меню
/// </summary>
public sealed record CreateOrUpdateMenuElementCommand
(
    Guid RecipeId,
    int ServeCount,
    DaysOfWeek? Date,
    Meal? Meal
);
