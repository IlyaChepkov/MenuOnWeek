using MenuOnWeek.Domain;

namespace MenuOnWeek.Application.Menus;

public sealed class CreateMenuModel
{
    public required string Name { get; set; }

    public MenuType MenuType { get; set; }

    public List<MenuElementModel> MenuRecipes { get; set; } = new List<MenuElementModel>();
}
