using MenuOnWeek.Data;
using MenuOnWeek.Domain.Recipes;
using Microsoft.EntityFrameworkCore;

namespace Data;

internal sealed class RecipeRepository : EntityWithIdRepository<Recipe>, IRecipeRepository
{
    public RecipeRepository(DataContext dataContext) : base(dataContext)
    {

    }

    public override async Task<IReadOnlyList<Recipe>> Get(int offset, int limit, CancellationToken token)
    {
        return await dataContext.Set<Recipe>()
            .AsNoTracking()
            .Include(x => x.RecipeIngredients)
            .ThenInclude(x => x.Ingredient)
            .ThenInclude(x => x.IngredientUnits)
            .Skip(offset)
            .Take(limit)
            .ToListAsync(token);
    }

    public override async Task<Recipe> GetById(Guid id, CancellationToken token)
    {
        var recipe = await ById(id)
            .Include(x => x.RecipeIngredients)
            .ThenInclude(x => x.Ingredient)
            .ThenInclude(x => x.IngredientUnits)
            .SingleOrDefaultAsync(token);
        if (recipe is not null)
        {
            return recipe;
        }
        throw new KeyNotFoundException("Рецепта с таким идентификатором не найдено");
    }

    public Task<Recipe?> GetByName(string name, CancellationToken token)
    {
        return dataContext.Set<Recipe>()
            .Include(x => x.RecipeIngredients)
            .ThenInclude(x => x.Ingredient)
            .ThenInclude(x => x.IngredientUnits)
            .SingleOrDefaultAsync(x => x.Name == name, token);
        
    }
}
