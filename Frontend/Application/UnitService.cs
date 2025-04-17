using Data;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application;

internal sealed class UnitService : IUnitService
{
    private readonly IUnitRepository unitRepository;

    public UnitService(IUnitRepository unitRepository)
    {
        this.unitRepository = unitRepository;
    }

    public void Add(Unit entity)
    {
        unitRepository.Add(entity);
    }

    public IReadOnlyList<Unit> GetAll(Func<Unit, bool> predicate, int offset, int limit)
    {
        return unitRepository.
            GetAll(predicate).Skip(offset).Take(limit).ToList();
    }

    public void Remove(Unit entity)
    {
        unitRepository.Remove(entity);
    }

    public void Update(Unit entity)
    {
        unitRepository.Update(entity);
    }
}
