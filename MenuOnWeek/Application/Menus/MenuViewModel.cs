namespace MenuOnWeek.Application.Menus;

public sealed class MenuViewModel
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public double Price { get; set; }

    public List<MenuElementViewModel> Recipes { get; set; } = new List<MenuElementViewModel>();
}
