namespace Data;

public interface IBaseRepository<TEntity>
{
    void Add(TEntity entity);

    IReadOnlyList<TEntity> GetAll(Func<TEntity, bool> predicate);

    void Update(TEntity entity);

    void Remove(TEntity entity);
}
