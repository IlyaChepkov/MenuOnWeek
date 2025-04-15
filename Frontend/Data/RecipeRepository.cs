using Domain;

namespace Data;

internal sealed class RecipeRepository : BaseRepository<Recipe>, IRecipeRepository
{
    public RecipeRepository(DataContext dataContext) : base(dataContext)
    {

    }
}
