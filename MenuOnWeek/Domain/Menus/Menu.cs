using MenuOnWeek.Utils;
using Utils;

namespace MenuOnWeek.Domain.Menus;

/// <summary>
/// Меню
/// </summary>
public sealed class Menu : IEntityWithId
{
    private List<MenuRecipes> menuRecipes = [];
    private string name;

    private Menu(string name, Guid id, MenuType menuType)
    {
        Id = id;
        this.name = name;
        MenuType = menuType;
    }

    /// <inheritdoc/>
    public Guid Id { get; set; }

    /// <summary>
    /// Название меню
    /// </summary>
    public string Name
    {
        get => name;    
        set
        {
            ValidationException<string>.ThrowIf(x => String.IsNullOrWhiteSpace(x), value, "Название не может быть пустым");
            name = value;
        }
    }

    /// <summary>
    /// Тип меню
    /// </summary>
    public MenuType MenuType { get; set; }

    /// <summary>
    /// Блюда меню
    /// </summary>
    public IReadOnlyList<MenuRecipes> MenuRecipes => menuRecipes;

    /// <summary>
    /// Цена меню
    /// </summary>
    public double Price => MenuRecipes.Sum(x => x.Recipe.Required().Price * x.Serve);

    public static Menu Create(string name, MenuType menuType)
    {
        ValidationException<string>.ThrowIf(x => String.IsNullOrWhiteSpace(x), name, "Название не может быть пустым");
        var menu = new Menu(name, Guid.NewGuid(), menuType);
        return menu;
    }
}
