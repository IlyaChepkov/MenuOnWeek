using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;

public sealed class Menu
{

    public Menu(string name, int id)
    {
        Name = name;
        Id = id;
    }

    /// <summary>
    /// Id меню
    /// </summary>
    public int Id { get; set; }

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

}
