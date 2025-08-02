using Data;
using Domain;
using MenuOnWeek.Application;

namespace MenuOnWeek.Data.Ingredients;

public interface IIngredientRepository : IBaseRepository<Ingredient>, IEntityWithIdRepository<Ingredient>
{
    Task<Ingredient?> GetByName(string name, CancellationToken token);

    Task<IReadOnlyList<Ingredient>> GetByPartName(string partName, CancellationToken token);
}
