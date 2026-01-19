using Data;
using MenuOnWeek.Application;
using MenuOnWeek.Domain;
using Microsoft.EntityFrameworkCore;
using Utils;

namespace MenuOnWeek.Data;

internal class EntityWithIdRepository<TEntity> :
    BaseRepository<TEntity>, IEntityWithIdRepository<TEntity> where TEntity : class, IEntityWithId
{
    public EntityWithIdRepository(DataContext dataContext) : base(dataContext)
    {

    }

    public async Task Remove(Guid id, CancellationToken token)
    {
        dataContext.Set<TEntity>().Remove(dataContext.Set<TEntity>().Single(x => x.Id == id));
        await dataContext.SaveChangesAsync(token);
    }

    public async Task RemoveRange(IReadOnlyList<Guid> ids, CancellationToken token)
    {
        dataContext.Set<TEntity>().RemoveRange(dataContext.Set<TEntity>().Where(x => ids.Any(y => y == x.Id)));
        await dataContext.SaveChangesAsync(token);
    }

    public virtual async Task<TEntity> GetById(Guid id, CancellationToken token)
    {
        var entity = await ById(id).SingleOrDefaultAsync(token);
        if (entity is null)
        {
            throw new KeyNotFoundException($"Сущность {typeof(TEntity).Name} с идентификатором {id} не найдена");
        }
        return entity;
    }

    protected IQueryable<TEntity> ById(Guid id)
    {
        return dataContext.Set<TEntity>().Where(x => x.Id == id);
    }
}
