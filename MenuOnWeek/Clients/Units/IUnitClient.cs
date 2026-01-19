using System.Net.Http;
using MenuOnWeek.Contracts.Units;

namespace MenuOnWeek.Clients.Units;

/// <summary>
/// Клиент для обращения к сервису единиц измерения
/// </summary>
public interface IUnitClient
{
    /// <summary>
    /// Добавляет единицу измерения
    /// </summary>
    public Task Add(UnitCreateRequest request, CancellationToken token);

    /// <summary>
    /// Возвращает все единицы измерения, начиная с offset и заканчиная limit
    /// </summary>
    public Task<IReadOnlyList<UnitResponse>> Get(int offset, int limit, CancellationToken token);

    /// <summary>
    /// Обновляет единицу измерения
    /// </summary>
    public Task Update(UnitUpdateRequest request, CancellationToken token);

    /// <summary>
    /// Удаляет единицу измерения
    /// </summary>
    public Task Remove(Guid? id, CancellationToken token);


    /// <summary>
    /// Возвращает единицу измерения по id
    /// </summary>
    public Task<UnitResponse> GetById(Guid? id, CancellationToken token);

    /// <summary>
    /// Вовращает единицу измерения поназванию
    /// </summary>
    public Task<UnitResponse?> GetByName(string? name, CancellationToken token);


    /// <summary>
    /// Вовращает все единицы измерения содержащие подстроку namePart в названии
    /// </summary>
    public Task<IReadOnlyList<UnitResponse>> GetByNamePart(
        string? namePart,
        int offset,
        int limit,
        CancellationToken token);

    /// <summary>
    /// Возвращает все все единицы измерения ингредиента
    /// </summary>
    public Task<IReadOnlyList<UnitResponse>> GetByIngredient(Guid? ingredientId, CancellationToken token);
}
