using System;
using System.Linq;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace MenuOnWeek.Data.Ingredients;

internal sealed class IngredientRepository : EntityWithIdRepository<Ingredient>, IIngredientRepository
{
    public IngredientRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public override IReadOnlyList<Ingredient> GetAll(Func<Ingredient, bool> predicate)
    {
        return dataContext.Set<Ingredient>()
            .AsNoTracking()
            .Include(x => x.Unit)
            .Include(x => x.IngredientUnits)
            .ThenInclude(x => x.Unit)
            .Where(predicate)
            .ToList();
    }

    public override Ingredient GetById(Guid id)
    {
        return dataContext.Set<Ingredient>()
            .Include(x => x.Unit)
            .Include(x => x.IngredientUnits)
            .ThenInclude(x => x.Unit)
            .Single(x => x.Id == id);
    }
}
