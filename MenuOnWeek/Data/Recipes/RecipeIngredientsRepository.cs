using Data;
using MenuOnWeek.Application.Recipes;
using MenuOnWeek.Domain.Recipes;

namespace MenuOnWeek.Data.Recipes;

internal sealed class RecipeIngredientsRepository : BaseRepository<RecipeIngredients>, IRecipeIngredientsRepository
{
    public RecipeIngredientsRepository(DataContext dataContext) : base(dataContext)
    {

    }
}
