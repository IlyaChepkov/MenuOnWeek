using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Ingredients;

public interface IIngredientService
{
    Task Add(CreateIngredientCommand entity, CancellationToken token);

    Task<IReadOnlyList<IngredientViewCommand>> GetAll(int offset, int limit, CancellationToken token);

    Task Update(UpdateIngredientCommand entity, CancellationToken token);

    Task Remove(Guid entity, CancellationToken token);

    Task<IngredientViewCommand> GetById(Guid id, CancellationToken token);

    Task<IngredientViewCommand?> GetByName(string name, CancellationToken token);

    Task<IReadOnlyList<IngredientViewCommand>> GetByPartName(string namePart, int offset, int limit, CancellationToken token);
}
