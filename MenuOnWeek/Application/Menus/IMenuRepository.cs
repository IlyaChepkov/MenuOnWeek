using Domain;
using MenuOnWeek.Application;

namespace Data;

public interface IMenuRepository : IEntityWithIdRepository<Menu>
{
    Task<Menu?> GetByName(string name, CancellationToken token);
}
