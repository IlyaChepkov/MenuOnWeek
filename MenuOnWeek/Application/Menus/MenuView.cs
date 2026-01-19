using MenuOnWeek.Domain.Menus;

namespace MenuOnWeek.Application.Menus;

/// <summary>
/// Модель для просмотра меню
/// </summary>
public sealed record MenuView( Guid Id,
      string Name,
     MenuType MenuType,
     double Price,
     List<MenuElementView> Recipes
    );
