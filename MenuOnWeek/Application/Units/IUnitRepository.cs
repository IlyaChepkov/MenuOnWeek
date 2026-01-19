using MenuOnWeek.Application;
using MenuOnWeek.Domain.Units;

namespace Data;

/// <summary>
/// Репозиторий для работы с единицами измерения
/// </summary>
public interface IUnitRepository : IBaseRepository<Unit>, IEntityWithIdRepository<Unit>
{
    /// <summary>
    /// Возвращает единицу измерения по названию
    /// </summary>
    public Task<Unit?> GetByName(string name, CancellationToken token);

    /// <summary>
    /// Возвращает список единиц измерения, названия которых содержат подстроку partName
    /// </summary>
    public Task<IReadOnlyList<Unit>> GetByPartName(
        string partName,
        int offset,
        int limit,
        CancellationToken token);
}
