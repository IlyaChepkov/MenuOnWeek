using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MenuOnWeek.Application.Recipes;

internal static class RecipeConverter
{
    internal static RecipeViewModel ConvertToRecipeViewModel(this Recipe recipe)
    {
        return new RecipeViewModel()
        {
            Name = recipe.Name,
            Image = recipe.Image,
            Price = recipe.Price,
            Description = recipe.Description,
            Ingredients = recipe.RecipeIngredients.Select(x => (x.IngredientId, new QuantityViewModel() {Count = x.Count, UnitId = x.UnitId })).ToDictionary()
        };
    }
}
