using System.Security.Cryptography.X509Certificates;
using Data;

using Domain;
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

    public async Task<IReadOnlyList<Unit>> GetByPartName(string partName, CancellationToken token)
    {
        return await dataContext.Set<Unit>().Where(x => x.Name.ToLower().Contains(partName.ToLower())).ToListAsync(token);
    }
}
