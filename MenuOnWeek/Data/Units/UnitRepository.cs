using Data;
using MenuOnWeek.Domain.Units;
using Microsoft.EntityFrameworkCore;

namespace MenuOnWeek.Data.Units;

internal sealed class UnitRepository : EntityWithIdRepository<Unit>, IUnitRepository
{
    public UnitRepository(DataContext dataContext) : base(dataContext)
    {

    }

    public Task<Unit?> GetByName(string name, CancellationToken token)
    {
        return dataContext.Set<Unit>().SingleOrDefaultAsync(x => x.Name == name, token);
    }

    public async Task<IReadOnlyList<Unit>> GetByPartName(string partName, int offset, int limit, CancellationToken token)
    {
        return await dataContext.Set<Unit>()
            .Where(x => x.Name.ToLower().Contains(partName.ToLower()))
            .Skip(offset)
            .Take(limit)
            .ToListAsync(token);
    }
}
