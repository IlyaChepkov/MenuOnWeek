using System;
using Utils;

namespace Data;

internal abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DataContext dataContext;

    public BaseRepository(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    public void Add(TEntity entity)
    {
        dataContext.Set<TEntity>().Add(entity);
        dataContext.SaveChanges();
    }

    public void AddRange(List<TEntity> entities)
    {
        dataContext.Set<TEntity>().AddRange(entities);
        dataContext.SaveChanges();
    }

    public virtual IReadOnlyList<TEntity> GetAll(Func<TEntity, bool> predicate)
    {
        return dataContext.Set<TEntity>().Where(predicate).ToList();
    }

    public virtual void Remove(TEntity entity)
    {
        dataContext.Set<TEntity>().Remove(entity);
        dataContext.SaveChanges();
    }

    public virtual void RemoveRange(List<TEntity> entities)
    {
        dataContext.Set<TEntity>().RemoveRange(entities);
        dataContext.SaveChanges();
    }

    public void Update(TEntity entity)
    {
        dataContext.Set<TEntity>().Update(entity);
        dataContext.SaveChanges();
    }

    public void UpdateRange(List<TEntity> entities)
    {
        dataContext.Set<TEntity>().UpdateRange(entities);
        dataContext.SaveChanges();
    }
}
