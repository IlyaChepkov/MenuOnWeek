using Data;
using Domain;
using MenuOnWeek.Application;

namespace MenuOnWeek.Data.Ingredients;

public interface IIngredientRepository : IBaseRepository<Ingredient>, IEntityWithIdRepository<Ingredient>
{
}
