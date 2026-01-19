namespace Application.Units;

public interface IUnitService
{
    /// <summary>
    /// Создает единицу измерения
    /// </summary>
    Task Add(CreateUnitCommand entity, CancellationToken token);

    /// <summary>
    /// Возвращает все единицы измерения начиная с offset и заканчивая limit
    /// </summary>
    Task<IReadOnlyList<UnitView>> Get(int offset, int limit, CancellationToken token);

    /// <summary>
    /// Обновляет единицу измерения
    /// </summary>
    Task Update(UpdateUnitCommand entity, CancellationToken token);

    /// <summary>
    /// Удаляет единицу измерения
    /// </summary>
    Task Remove(Guid id, CancellationToken token);

    /// <summary>
    /// Возвращает единицу измерения по идентификатору
    /// </summary>
    Task<UnitView> GetById(Guid id, CancellationToken token);

    /// <summary>
    /// Возвращает единицу измерения по названию
    /// </summary>
    public Task<UnitView?> GetByName(string name, CancellationToken token);

    /// <summary>
    /// Возвращает все единицы измерения названия которых содержат namePart
    /// </summary>
    public Task<IReadOnlyList<UnitView>> GetByNamePart(string namePart, int offset, int limit, CancellationToken token);

    /// <summary>
    /// Возвращает все единицы измерения для ингредиента с идентификатором ingredientId
    /// </summary>
    Task<IReadOnlyList<UnitView>> GetByIngredient(Guid ingredientId, CancellationToken token);
}
