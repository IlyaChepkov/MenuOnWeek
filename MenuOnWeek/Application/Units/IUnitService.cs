using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Units;

public interface IUnitService
{
    void Add(CreateUnitModel entity);

    IReadOnlyList<UnitViewModel> GetAll(int offset, int limit);

    void Update(UpdateUnitModel entity);

    void Remove(Guid id);

    UnitViewModel GetById(Guid id);

    UnitViewModel? GetByName(string name);

    IReadOnlyList<UnitViewModel> GetByNamePart(string namePart, int offset, int limit);

    IReadOnlyList<UnitViewModel> GetByIngredient(Guid ingredientId);
}
