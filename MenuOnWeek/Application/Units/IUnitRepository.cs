using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using MenuOnWeek.Application;

namespace Data;

public interface IUnitRepository : IBaseRepository<Unit>, IEntityWithIdRepository<Unit>
{
    public Task<Unit?> GetByName(string name, CancellationToken token);

    Task<IReadOnlyList<Unit>> GetByPartName(string partName, CancellationToken token);
}
