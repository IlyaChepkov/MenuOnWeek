using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using MenuOnWeek.Application.Recipes;
using MenuOnWeek.Domain;

namespace MenuOnWeek.Data.Recipes;

internal sealed class RecipeIngredientsRepository : BaseRepository<RecipeIngredients>, IRecipeIngredientsRepository
{
    public RecipeIngredientsRepository(DataContext dataContext) : base(dataContext)
    {
        
    }
}
