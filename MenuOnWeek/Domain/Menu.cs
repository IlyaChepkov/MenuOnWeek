using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MenuOnWeek.Domain;
using Utils;

namespace Domain;

public sealed class Menu : IEntityWithId
{
    private List<MenuRecipes> menuRecipes = [];

    private Menu(string name, Guid id, MenuType menuType)
    {
        Id = id;
        Name = name;
        MenuType = menuType;
    }

    /// <summary>
    /// Id меню
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название меню
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Тип меню
    /// </summary>
    public MenuType MenuType { get; set; }

    public  IReadOnlyList<MenuRecipes> MenuRecipes => menuRecipes;

    /// <summary>
    /// Цена меню
    /// </summary>
    public double Price => MenuRecipes.Sum(x => x.Recipe.Required().Price * x.Serve);

    public static Menu Create(string name, MenuType menuType)
    {
        var menu = new Menu(name, Guid.NewGuid(), menuType);
        return menu;
    }
}
