namespace MenuOnWeek.Application.Menus;

public sealed class CreateMenuModel
{
    public required string Name { get; set; }

    public List<MenuElementModel> Recipes { get; set; } = new List<MenuElementModel>();
}
