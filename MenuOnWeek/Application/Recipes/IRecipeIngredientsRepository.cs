using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using MenuOnWeek.Domain;

namespace MenuOnWeek.Application.Recipes;

public interface IRecipeIngredientsRepository : IBaseRepository<RecipeIngredients>
{
}
