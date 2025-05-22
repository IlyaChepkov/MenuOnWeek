using Data;
using Domain;

namespace Application;

internal sealed class MenuService : IMenuService
{
    private readonly IMenuRepository menuRepository;

    public MenuService(IMenuRepository menuRepository)
    {
        this.menuRepository = menuRepository;
    }

    public void Add(Menu entity)
    {
        menuRepository.Add(entity);
    }

    public IReadOnlyList<Menu> GetAll(Func<Menu, bool> predicate, int offset, int limit)
    {
        return menuRepository.
            GetAll(predicate).Skip(offset).Take(limit).ToList();
    }

    public void Remove(Menu entity)
    {
        menuRepository.Remove(entity);
    }

    public void Update(Menu entity)
    {
        menuRepository.Update(entity);
    }
}
