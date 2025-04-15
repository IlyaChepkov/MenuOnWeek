namespace Data;

internal abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
{
    private readonly DataContext dataContext;

    public BaseRepository(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    public void Add(TEntity entity)
    {
        GetDataSet().Add(entity);
        dataContext.Save();
    }

    public IReadOnlyList<TEntity> GetAll(Func<TEntity, bool> predicate)
    {
        return GetDataSet().Where(predicate).ToList();
    }

    public void Remove(TEntity entity)
    {
        GetDataSet().Remove(entity);
        dataContext.Save();
    }

    public void Update(TEntity entity)
    {
        dataContext.Save();
    }

    private List<TEntity> GetDataSet()
    {
        return dataContext
            .GetType()
            .GetProperties()
            .Single(x => x.PropertyType == typeof(List<TEntity>))
            .GetMethod.Invoke(dataContext, null) as List<TEntity>;
    }
}
