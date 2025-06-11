// Ignore Spelling: Repository

using Domain;

namespace Data;

internal sealed class UnitRepository : BaseRepository<Unit>, IUnitRepository
{
    public UnitRepository(DataContext dataContext) : base(dataContext)
    {
        
    }
}
