using System.Security.Cryptography.X509Certificates;
using Data;

using Domain;

namespace MenuOnWeek.Data.Units;

internal sealed class UnitRepository : EntityWithIdRepository<Unit>, IUnitRepository
{
    public UnitRepository(DataContext dataContext) : base(dataContext)
    {
        
    }
}
