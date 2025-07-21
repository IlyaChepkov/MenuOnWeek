using MenuOnWeek.Application.Menus;
using MenuOnWeek.Domain;

namespace MenuOnWeek.Frontend.Menu;

internal sealed record MenuDto( string Name, List<MenuElementDto> Recipes, MenuType MenuType);
