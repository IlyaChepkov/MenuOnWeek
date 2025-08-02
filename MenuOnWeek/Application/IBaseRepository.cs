namespace Data;

public interface IBaseRepository<TEntity>
{
    public Task Add(TEntity entity, CancellationToken token);

    public Task AddRange(List<TEntity> entities, CancellationToken token);

    public Task<IReadOnlyList<TEntity>> GetAll(CancellationToken token);

    public Task Update(TEntity entity, CancellationToken token);

    public Task UpdateRange(List<TEntity> entities, CancellationToken token);

    public Task Remove(TEntity entity, CancellationToken token);

    public Task RemoveRange(List<TEntity> entities, CancellationToken token);
}
