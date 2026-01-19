using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MenuOnWeek.Contracts;
using MenuOnWeek.Contracts.Ingredients;
using MenuOnWeek.Utils;
using Utils;

namespace MenuOnWeek.Clients.Ingredients;

public interface IIngredientClient
{
    public Task Add(IngredientCreateRequest request, CancellationToken token);

    /// <summary>
    /// Возвращает все ингредиенты, начиная с offset и заканчиная limit
    /// </summary>
    public Task<IReadOnlyList<IngredientResponse>> GetAll(int offset, int limit, CancellationToken token);

    /// <summary>
    /// Обновляет ингредиент
    /// </summary>
    public Task Update(IngredientUpdateRequest request, CancellationToken token);

    /// <summary>
    /// Удаляет ингредиент
    /// </summary>
    public Task Remove(Guid? id, CancellationToken token);

    /// <summary>
    /// Возвращает ингредиент по id
    /// </summary>
    public Task<IngredientResponse> GetById(Guid? id, CancellationToken token);

    /// <summary>
    /// Возвращает ингредиент по названию
    /// </summary>
    public Task<IngredientResponse?> GetByName(string name, CancellationToken token);

    /// <summary>
    /// Вовращает все ингредиенты содержащие подстроку namePart в названии
    /// </summary>
    public Task<IReadOnlyList<IngredientResponse>> GetByPartName(string? namePart, int offset, int limit, CancellationToken token);
}
