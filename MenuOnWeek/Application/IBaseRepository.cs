namespace Data;

public interface IBaseRepository<TEntity>
{
    void Add(TEntity entity);

    void AddRange(List<TEntity> entities);

    IReadOnlyList<TEntity> GetAll(Func<TEntity, bool> predicate);

    void Update(TEntity entity);

    void UpdateRange(List<TEntity> entities);

    void Remove(TEntity entity);

    void RemoveRange(List<TEntity> entities);
}
