using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;

public sealed class Menu
{

    public Menu(string name, List<MenuElement> recipes, Guid id)
    {
        Id = id;
        Name = name;
        Recipes = recipes;
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
    /// Цена меню
    /// </summary>
    public double Price { get{ return Recipes
                .Select(x => x.Recipe.Price * x.Serve).Sum(); } }

    /// <summary>
    /// Рецепты в меню
    /// </summary>
    public List<MenuElement> Recipes { get; set; } = new List<MenuElement>();

    public static Menu Create(string name, List<MenuElement> recipes)
    {
        var menu = new Menu(name, recipes, Guid.NewGuid());
        return menu;
    }
}
