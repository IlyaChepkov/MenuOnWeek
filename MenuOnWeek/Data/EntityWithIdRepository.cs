using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using MenuOnWeek.Application;
using MenuOnWeek.Domain;
using Microsoft.EntityFrameworkCore;

namespace MenuOnWeek.Data;

internal class EntityWithIdRepository<TEntity> : BaseRepository<TEntity>, IEntityWithIdRepository<TEntity> where TEntity : class, IEntityWithId
{
    public EntityWithIdRepository(DataContext dataContext) : base(dataContext)
    {

    }

    public async Task Remove(Guid id, CancellationToken token)
    {
        dataContext.Set<TEntity>().Remove(dataContext.Set<TEntity>().Single(x => x.Id == id));
        await dataContext.SaveChangesAsync(token);
    }

    public async Task RemoveRange(List<Guid> id, CancellationToken token)
    {
        dataContext.Set<TEntity>().RemoveRange(dataContext.Set<TEntity>().Where(x => id.Any(y => y == x.Id)));
        await dataContext.SaveChangesAsync();
    }

    public virtual Task<TEntity> GetById(Guid id, CancellationToken token)
    {
        return dataContext.Set<TEntity>().SingleAsync(x => x.Id == id, token);
    }
}
