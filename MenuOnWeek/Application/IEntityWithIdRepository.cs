using Data;
using MenuOnWeek.Domain;

namespace MenuOnWeek.Application;

public interface IEntityWithIdRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntityWithId
{
    Task<TEntity> GetById(Guid id, CancellationToken token);
}
