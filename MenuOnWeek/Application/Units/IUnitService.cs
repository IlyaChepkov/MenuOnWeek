using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Units;

public interface IUnitService
{
    Task Add(CreateUnitCommand entity, CancellationToken token);

    Task<IReadOnlyList<UnitViewCommand>> GetAll(int offset, int limit, CancellationToken token);

    Task Update(UpdateUnitCommand entity, CancellationToken token);

    Task Remove(Guid id, CancellationToken token);

    Task<UnitViewCommand> GetById(Guid id, CancellationToken token);

    Task<UnitViewCommand?> GetByName(string name, CancellationToken token);

    Task<IReadOnlyList<UnitViewCommand>> GetByNamePart(string namePart, int offset, int limit, CancellationToken token);

    Task<IReadOnlyList<UnitViewCommand>> GetByIngredient(Guid ingredientId, CancellationToken token);
}
