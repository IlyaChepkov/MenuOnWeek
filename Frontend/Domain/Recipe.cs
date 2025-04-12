using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    internal class Recipe
    {

        public Recipe(string name, Dictionary<Ingredient, int> ingredients, int id)
        {
            Name = name;
            Id = id;
            Ingredients = ingredients;

            Price = Ingredients.Select(x => x.Key.Price * x.Value).ToList().Sum();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public Dictionary<Ingredient, int> Ingredients { get; set; }
    }
}
