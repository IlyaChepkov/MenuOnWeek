using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    internal class MenuOnDay
    {

        public MenuOnDay(string name, int id, List<Recipe> recipes)
        {
            Name = name;
            Id = id;
            Recipes = recipes;
            Price = Recipes.Select(x => x.Price).ToList().Sum();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public List<Recipe> Recipes { get; set; }

    }
}
