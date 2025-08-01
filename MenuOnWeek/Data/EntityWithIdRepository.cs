using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using MenuOnWeek.Application;
using MenuOnWeek.Domain;

namespace MenuOnWeek.Data;

internal class EntityWithIdRepository<TEntity> : BaseRepository<TEntity>, IEntityWithIdRepository<TEntity> where TEntity : class, IEntityWithId
{
    public EntityWithIdRepository(DataContext dataContext) : base(dataContext)
    {

    }

    public void Remove(Guid id)
    {
        dataContext.Set<TEntity>().Remove(dataContext.Set<TEntity>().Single(x => x.Id == id));
        dataContext.SaveChanges();
    }

    public void RemoveRange(List<Guid> id)
    {
        dataContext.Set<TEntity>().RemoveRange(dataContext.Set<TEntity>().Where(x => id.Any(y => y == x.Id)));
        dataContext.SaveChanges();
    }

    public virtual TEntity GetById(Guid id)
    {
        return dataContext.Set<TEntity>().Single(x => x.Id == id);
    }
}
