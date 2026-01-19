namespace Data;

/// <summary>
/// Базовый репозиторий
/// </summary>
public interface IBaseRepository<TEntity>
{
    /// <summary>
    /// Добавляет сущность
    /// </summary>
    public Task Add(TEntity entity, CancellationToken token);

    /// <summary>
    /// Добавляет список сущностей
    /// </summary>
    public Task AddRange(IReadOnlyList<TEntity> entities, CancellationToken token);

    /// <summary>
    /// Возвращает все сущности начиная с offset и заканчивая limit
    /// </summary>
    public Task<IReadOnlyList<TEntity>> Get(int offset, int limit, CancellationToken token);

    /// <summary>
    /// Обновляет сущность
    /// </summary>
    public Task Update(TEntity entity, CancellationToken token);

    /// <summary>
    /// Обновляет список сущностей
    /// </summary>
    public Task UpdateRange(IReadOnlyList<TEntity> entities, CancellationToken token);

    /// <summary>
    /// Удаляет сущность
    /// </summary>
    public Task Remove(TEntity entity, CancellationToken token);

    /// <summary>
    /// Удаляет список сущностей
    /// </summary>
    public Task RemoveRange(IReadOnlyList<TEntity> entities, CancellationToken token);

    /// <summary>
    /// Возвращает количество сущностей
    /// </summary>
    public Task<int> Count(CancellationToken token);
}
