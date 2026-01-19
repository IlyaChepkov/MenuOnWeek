using MenuOnWeek.Domain.Menus;

namespace MenuOnWeek.Application.Menus;

/// <summary>
/// комманда создания меню
/// </summary>
public sealed record CreateMenuCommand(
    string Name,
    MenuType MenuType,
    List<CreateOrUpdateMenuElementCommand> MenuRecipes
);
