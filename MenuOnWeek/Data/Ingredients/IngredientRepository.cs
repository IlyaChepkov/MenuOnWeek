using System.Xml.Linq;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace MenuOnWeek.Data.Ingredients;

internal sealed class IngredientRepository : EntityWithIdRepository<Ingredient>, IIngredientRepository
{
    public IngredientRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public override async Task<IReadOnlyList<Ingredient>> GetAll(CancellationToken token)
    {
        return await dataContext.Set<Ingredient>()
            .AsNoTracking()
            .Include(x => x.Unit)
            .Include(x => x.IngredientUnits)
            .ThenInclude(x => x.Unit)
            .ToListAsync(token);
    }

    public override Task<Ingredient> GetById(Guid id, CancellationToken token)
    {
        return dataContext.Set<Ingredient>()
            .Include(x => x.Unit)
            .Include(x => x.IngredientUnits)
            .ThenInclude(x => x.Unit)
            .SingleAsync(x => x.Id == id, token);
    }

    public Task<Ingredient?> GetByName(string name, CancellationToken token)
    {
        return dataContext.Set<Ingredient>()
            .Include(x => x.Unit)
            .Include(x => x.IngredientUnits)
            .ThenInclude(x => x.Unit)
            .SingleOrDefaultAsync(x => x.Name == name, token);
    }

    public async Task<IReadOnlyList<Ingredient>> GetByPartName(string partName, CancellationToken token)
    {
        return await dataContext.Set<Ingredient>()
            .Include(x => x.Unit)
            .Include(x => x.IngredientUnits)
            .ThenInclude(x => x.Unit)
            .Where(x => x.Name.ToLower().Contains(partName)).ToListAsync(token);
    }
}
