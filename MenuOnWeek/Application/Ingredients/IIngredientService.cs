namespace Application.Ingredients;

/// <summary>
/// Сервис для работы с ингредиентами
/// </summary>
public interface IIngredientService
{
    /// <summary>
    /// Добавляет ингредиент
    /// </summary>
    Task Add(CreateIngredientCommand entity, CancellationToken token);

    /// <summary>
    /// Возвращает все ингредиенты начиная с offset и заканчивая limit
    /// </summary>
    Task<IReadOnlyList<IngredientView>> GetAll(int offset, int limit, CancellationToken token);

    /// <summary>
    /// Обновляет ингредиент
    /// </summary>
    Task Update(UpdateIngredientCommand entity, CancellationToken token);

    /// <summary>
    /// Удаляет ингредиент
    /// </summary>
    Task Remove(Guid entity, CancellationToken token);

    /// <summary>
    /// Возвращает ингредиент по идентификатору
    /// </summary>
    Task<IngredientView> GetById(Guid id, CancellationToken token);

    /// <summary>
    /// Возвращает ингредиент по названию
    /// </summary>
    Task<IngredientView?> GetByName(string name, CancellationToken token);

    /// <summary>
    /// Возвращает все ингредиенты содержащие namePart в названии
    /// </summary>
    Task<IReadOnlyList<IngredientView>> GetByPartName(string namePart, int offset, int limit, CancellationToken token);
}
