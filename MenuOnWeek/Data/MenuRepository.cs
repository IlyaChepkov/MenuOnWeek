using Domain;

namespace Data;

internal sealed class MenuRepository : BaseRepository<Menu>, IMenuRepository
{
    public MenuRepository(DataContext dataContext) : base(dataContext)
    {

    }
}
