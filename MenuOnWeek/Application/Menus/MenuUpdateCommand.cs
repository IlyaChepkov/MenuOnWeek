using MenuOnWeek.Domain.Menus;

namespace MenuOnWeek.Application.Menus;

/// <summary>
/// Комманда обновления меню
/// </summary>
public sealed record MenuUpdateCommand
(
     Guid Id,
    string Name,
    MenuType MenuType,
    List<CreateOrUpdateMenuElementCommand> MenuRecipes
);
