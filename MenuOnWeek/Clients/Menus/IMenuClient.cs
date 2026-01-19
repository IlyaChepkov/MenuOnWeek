using MenuOnWeek.Contracts;
using MenuOnWeek.Contracts.Menus;
using MenuOnWeek.Domain.Menus;
using MenuOnWeek.Utils;
using Utils;

namespace MenuOnWeek.Clients.Menus;

public interface IMenuClient
{
    /// <summary>
    /// Добавляет меню
    /// </summary>
    public Task Add(MenuCreateRequest request, CancellationToken token);

    /// <summary>
    /// Возвращает все меню, начиная с offset и заканчивая limit
    /// </summary>
    public Task<IReadOnlyList<MenuResponse>> GetAll(int offset, int limit, CancellationToken token);

    /// <summary>
    /// Обновляет меню
    /// </summary>
    public Task Update(MenuUpdateRequest request, CancellationToken token);

    /// <summary>
    /// Удаляет меню
    /// </summary>
    public Task Remove(Guid? id, CancellationToken token);

    /// <summary>
    /// Возвращает меню по названию
    /// </summary>
    public Task<MenuResponse?> GetByName(string? name, CancellationToken token);
}
