using Data;
using MenuOnWeek.Application;
using MenuOnWeek.Domain.Ingredients;

namespace MenuOnWeek.Data.Ingredients;

/// <summary>
/// Репозиторий для работы с ингредиентами
/// </summary>
public interface IIngredientRepository : IBaseRepository<Ingredient>, IEntityWithIdRepository<Ingredient>
{
    /// <summary>
    /// Возвращает ингредиент по названию
    /// </summary>
    public Task<Ingredient?> GetByName(string name, CancellationToken token);

    /// <summary>
    /// Возвращает список ингредиентов по части названия
    /// </summary>
    public Task<IReadOnlyList<Ingredient>> GetByPartName(string partName, int offset, int limit, CancellationToken token);
}
