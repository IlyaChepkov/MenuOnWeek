namespace MenuOnWeek.Application.Recipes;

/// <summary>
/// Сервис для работы с рецептами
/// </summary>
public interface IRecipeService
{
    /// <summary>
    /// Добавляет рецепт
    /// </summary>
    Task Add(RecipeCreateCommand entity, CancellationToken token);

    /// <summary>
    /// Возвращает все рецепты начиная с offset и заканчивая limit
    /// </summary>
    Task<IReadOnlyList<RecipeView>> Get(int offset, int limit, CancellationToken token);

    /// <summary>
    /// Обновляет рецепт
    /// </summary>
    Task Update(RecipeUpdateCommand entity, CancellationToken token);

    /// <summary>
    /// Удаляет рецепт
    /// </summary>
    Task Remove(Guid id, CancellationToken token);

    /// <summary>
    /// Возвращает рецепт по идентификатору
    /// </summary>
    Task<RecipeView> GetById(Guid id, CancellationToken token);

    /// <summary>
    /// Возвращает рецепт по названию
    /// </summary>
    Task<RecipeView?> GetByName(string name, CancellationToken token);
}
