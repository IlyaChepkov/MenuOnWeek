using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Units;
using Domain;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Application.Ingredients;

internal static class IngredientConverters
{
    internal static IngredientViewModel ConvertToIngredientViewModel(this Ingredient ingredient)
    {
        return new IngredientViewModel()
        {
            Id = ingredient.Id,
            Name = ingredient.Name,
            Price = ingredient.Price,
            Table = ingredient.Table
               .Select(y => (y.Value, new UnitViewModel() { Id = y.Key.Id, Name = y.Key.Name }))
               .ToDictionary(y => y.Item2, y => y.Item1),
            UnitId = ingredient.UnitId
        };
    }
}
