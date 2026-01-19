using Data;
using MenuOnWeek.Domain.Ingredients;
using Microsoft.EntityFrameworkCore;

namespace MenuOnWeek.Data.Ingredients;

internal sealed class IngredientRepository : EntityWithIdRepository<Ingredient>, IIngredientRepository
{
    public IngredientRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public override async Task<IReadOnlyList<Ingredient>> Get(int offset, int limit, CancellationToken token)
    {
        return await dataContext.Set<Ingredient>()
            .AsNoTracking()
            .Include(x => x.Unit)
            .Include(x => x.IngredientUnits)
            .ThenInclude(x => x.Unit)
            .ToListAsync(token);
    }

    public override async Task<Ingredient> GetById(Guid id, CancellationToken token)
    {
        var ingredient = await ById(id)
            .Include(x => x.Unit)
            .Include(x => x.IngredientUnits)
            .ThenInclude(x => x.Unit)
            .SingleOrDefaultAsync(token);
        if (ingredient is not null)
        {
            return ingredient;
        }
        throw new KeyNotFoundException("Ингредиента с таким идентификатором не найдено");
    }

    public Task<Ingredient?> GetByName(string name, CancellationToken token)
    {
        return dataContext.Set<Ingredient>()
            .Include(x => x.Unit)
            .Include(x => x.IngredientUnits)
            .ThenInclude(x => x.Unit)
            .SingleOrDefaultAsync(x => x.Name == name, token);
    }

    public async Task<IReadOnlyList<Ingredient>> GetByPartName(string partName, int offset, int limit, CancellationToken token)
    {
        return await dataContext.Set<Ingredient>()
            .Include(x => x.Unit)
            .Include(x => x.IngredientUnits)
            .ThenInclude(x => x.Unit)
            .Where(x => x.Name.ToLower().Contains(partName))
            .Skip(offset)
            .Take(limit)
            .ToListAsync(token);
    }
}
