using MenuOnWeek.Application;
using MenuOnWeek.Domain.Menus;

namespace Data;

/// <summary>
/// Репозиторий для работы с меню
/// </summary>
public interface IMenuRepository : IEntityWithIdRepository<Menu>
{
    /// <summary>
    /// Возвращает меню по названию
    /// </summary>
    public Task<Menu?> GetByName(string name, CancellationToken token);
}
