using Domain;

namespace Data;

internal sealed class IngredientRepository : BaseRepository<Ingredient>, IIngredientRepository
{
    public IngredientRepository(DataContext dataContext) : base(dataContext)
    {

    }
}
