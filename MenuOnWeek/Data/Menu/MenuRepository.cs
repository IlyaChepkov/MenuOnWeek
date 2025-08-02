using Domain;
using MenuOnWeek.Data;
using Microsoft.EntityFrameworkCore;
using Utils;

namespace Data;

internal sealed class MenuRepository : EntityWithIdRepository<Menu>, IMenuRepository
{
    public MenuRepository(DataContext dataContext) : base(dataContext)
    {

    }

    public override async Task<IReadOnlyList<Menu>> GetAll(CancellationToken token)
    {
        return await dataContext.Set<Menu>()
            .AsNoTracking()
            .Include(x => x.MenuRecipes)
            .ThenInclude(x => x.Recipe)
            .ToListAsync(token);
    }

    public Task<Menu?> GetByName(string name, CancellationToken token)
    {
        return dataContext.Set<Menu>()
            .AsNoTracking()
            .Include(x => x.MenuRecipes)
            .ThenInclude(x => x.Recipe)
            .SingleOrDefaultAsync(x => x.Name == name, token);
    }
}
