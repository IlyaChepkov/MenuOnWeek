using MenuOnWeek.Application;
using MenuOnWeek.Domain.Recipes;

namespace Data;

/// <summary>
/// Репозиторий для работы с рецептами
/// </summary>
public interface IRecipeRepository : IEntityWithIdRepository<Recipe>
{
    /// <summary>
    /// Возвращает рецепт по имени
    /// </summary>
    public Task<Recipe?> GetByName(string name, CancellationToken token);
}
