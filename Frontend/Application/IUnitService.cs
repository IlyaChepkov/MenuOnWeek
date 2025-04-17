using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application;

public interface IUnitService
{
    void Add(Unit entity);

    IReadOnlyList<Unit> GetAll(Func<Unit, bool> predicate, int offset, int limit);

    void Update(Unit entity);

    void Remove(Unit entity);
}
