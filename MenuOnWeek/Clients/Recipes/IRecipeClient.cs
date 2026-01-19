using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuOnWeek.Contracts;
using MenuOnWeek.Contracts.Recipes;
using MenuOnWeek.Utils;
using Utils;

namespace MenuOnWeek.Clients.Recipes;

public interface IRecipeClient
{
    /// <summary>
    /// Добавляет рецепт
    /// </summary>
    public Task Add(RecipeCreateRequest request, CancellationToken token);

    /// <summary>
    /// Возвращает все рецепты, начиная с offset и заканчивая limit
    /// </summary>
    public Task<IReadOnlyList<RecipeResponse>> GetAll(int offset, int limit, CancellationToken token);

    /// <summary>
    /// Обновляет рецепт
    /// </summary>
    public Task Update(RecipeUpdateRequest request, CancellationToken token);

    /// <summary>
    /// Удаляет рецепт
    /// </summary>
    public Task Remove(Guid? id, CancellationToken token);

    /// <summary>
    /// Возвращает рецепт по id
    /// </summary>
    public Task<RecipeResponse> GetById(Guid? id, CancellationToken token);

    /// <summary>
    /// Возвращает рецепт по имени
    /// </summary>
    public Task<RecipeResponse?> GetByName(string? name, CancellationToken token);
}
