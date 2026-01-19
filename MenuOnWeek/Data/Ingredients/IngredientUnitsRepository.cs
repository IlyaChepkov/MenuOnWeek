using Data;
using MenuOnWeek.Application.Ingredients;
using MenuOnWeek.Domain.Ingredients;

namespace MenuOnWeek.Data.Ingredients;

internal sealed class IngredientUnitsRepository : BaseRepository<IngredientUnits>, IIngredientUnitsRepository
{
    public IngredientUnitsRepository(DataContext dataContext) : base(dataContext)
    {

    }
}
