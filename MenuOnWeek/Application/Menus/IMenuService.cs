namespace MenuOnWeek.Application.Menus;

/// <summary>
/// Сервис для работы с меню
/// </summary>
public interface IMenuService
{
    /// <summary>
    /// Создает меню
    /// </summary>
    Task Add(CreateMenuCommand entity, CancellationToken token);

    /// <summary>
    /// Возвращает все меню начиная с offset и заканчивая limit
    /// </summary>
    Task<IReadOnlyList<MenuView>> GetAll(int offset, int limit, CancellationToken token);

    /// <summary>
    /// Обновляет меню
    /// </summary>
    Task Update(MenuUpdateCommand entity, CancellationToken token);

    /// <summary>
    /// Удаляет меню
    /// </summary>
    Task Remove(Guid entity, CancellationToken token);

    /// <summary>
    /// Возвращает меню по названию
    /// </summary>
    Task<MenuView?> GetByName(string name, CancellationToken token);
}
