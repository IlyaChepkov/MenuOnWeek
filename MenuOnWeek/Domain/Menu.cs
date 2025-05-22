using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;

public sealed class Menu
{

    public Menu(string name)
    {
        Name = name;
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
    public int Price { get{ return Recipes
                .Select(x => x.Recipe.Price * x.Serve).Sum(); } }

    /// <summary>
    /// Рецепты в меню
    /// </summary>
    public List<MenuElement> Recipes { get; set; } = new List<MenuElement>();

    public static Menu Create(string name)
    {
        var ingredient = new Menu(name);
        ingredient.Id = Guid.NewGuid();
        return ingredient;
    }
}
