using MenuOnWeek.Domain;

namespace MenuOnWeek.Application.Menus;

public sealed class MenuUpdateModel
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public MenuType MenuType { get; set; }

    public double Price { get; set; }

    public List<MenuElementModel> MenuRecipes { get; set; } = new List<MenuElementModel>();
}
