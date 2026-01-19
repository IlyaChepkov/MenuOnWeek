using Data;
using MenuOnWeek.Domain;

namespace MenuOnWeek.Application;

/// <summary>
/// Репозиторий для сущностей с идентификатором
/// </summary>
public interface IEntityWithIdRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntityWithId
{
    /// <summary>
    /// Возвращает сущность по идентификатору
    /// </summary>
    public Task<TEntity> GetById(Guid id, CancellationToken token);
}
