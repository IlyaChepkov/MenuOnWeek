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

    public override IReadOnlyList<Menu> GetAll(Func<Menu, bool> predicate)
    {
        return dataContext.Set<Menu>()
            .AsNoTracking()
            .Include(x => x.MenuRecipes)
            .ThenInclude(x => x.Recipe)
            .Where(predicate)
            .ToList();
    }
}
