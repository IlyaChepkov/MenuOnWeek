using Microsoft.EntityFrameworkCore;

namespace Data;

internal abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DataContext dataContext;

    public BaseRepository(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    public async Task Add(TEntity entity, CancellationToken token)
    {
        await dataContext.Set<TEntity>().AddAsync(entity, token);
        await dataContext.SaveChangesAsync(token);
    }

    public async Task AddRange(IReadOnlyList<TEntity> entities, CancellationToken token)
    {
        await dataContext.Set<TEntity>().AddRangeAsync(entities, token);
        await dataContext.SaveChangesAsync(token);
    }

    public virtual async Task<IReadOnlyList<TEntity>> Get(int offset, int limit, CancellationToken token)
    {
        return await dataContext.Set<TEntity>()
            .Skip(offset)
            .Take(limit)
            .ToListAsync(token);
    }

    public async Task Remove(TEntity entity, CancellationToken token)
    {
        dataContext.Set<TEntity>().Remove(entity);
        await dataContext.SaveChangesAsync(token);
    }

    public async Task RemoveRange(IReadOnlyList<TEntity> entities, CancellationToken token)
    {
        dataContext.Set<TEntity>().RemoveRange(entities);
        await dataContext.SaveChangesAsync(token);
    }

    public async Task Update(TEntity entity, CancellationToken token)
    {
        dataContext.Set<TEntity>().Update(entity);
        await dataContext.SaveChangesAsync(token);
    }

    public async Task UpdateRange(IReadOnlyList<TEntity> entities, CancellationToken token)
    {
        dataContext.Set<TEntity>().UpdateRange(entities);
        await dataContext.SaveChangesAsync(token);
    }


    public Task<int> Count(CancellationToken token)
    {
        return dataContext.Set<TEntity>().CountAsync(token);
    }
}
