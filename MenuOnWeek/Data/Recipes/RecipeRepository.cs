using Domain;
using MenuOnWeek.Data;
using Microsoft.EntityFrameworkCore;

namespace Data;

internal sealed class RecipeRepository : EntityWithIdRepository<Recipe>, IRecipeRepository
{
    public RecipeRepository(DataContext dataContext) : base(dataContext)
    {

    }

    public override async Task<IReadOnlyList<Recipe>> GetAll(CancellationToken token)
    {
        return await dataContext.Set<Recipe>()
            .AsNoTracking()
            .Include(x => x.RecipeIngredients)
            .ThenInclude(x => x.Ingredient)
            .ThenInclude(x => x.IngredientUnits)
            .ToListAsync();
    }

    public override Task<Recipe> GetById(Guid id, CancellationToken token)
    {
        return dataContext.Set<Recipe>()
            .Include(x => x.RecipeIngredients)
            .ThenInclude(x => x.Ingredient)
            .ThenInclude(x => x.IngredientUnits).SingleAsync(x => x.Id == id, token);
    }

    public Task<Recipe?> GetByName(string name, CancellationToken token)
    {
        return dataContext.Set<Recipe>()
            .Include(x => x.RecipeIngredients)
            .ThenInclude(x => x.Ingredient)
            .ThenInclude(x => x.IngredientUnits).SingleOrDefaultAsync(x => x.Name == name, token);
        
    }
}
