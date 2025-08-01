using Domain;
using MenuOnWeek.Data;
using Microsoft.EntityFrameworkCore;

namespace Data;

internal sealed class RecipeRepository : EntityWithIdRepository<Recipe>, IRecipeRepository
{
    public RecipeRepository(DataContext dataContext) : base(dataContext)
    {

    }

    public override IReadOnlyList<Recipe> GetAll(Func<Recipe, bool> predicate)
    {
        return dataContext.Set<Recipe>()
            .AsNoTracking()
            .Include(x => x.RecipeIngredients)
            .ThenInclude(x => x.Ingredient)
            .ThenInclude(x => x.IngredientUnits)
            .Where(predicate).ToList();
    }

    public override Recipe GetById(Guid id)
    {
        return dataContext.Set<Recipe>()
            .Include(x => x.RecipeIngredients)
            .ThenInclude(x => x.Ingredient)
            .ThenInclude(x => x.IngredientUnits).Single(x => x.Id == id);
    }
}
