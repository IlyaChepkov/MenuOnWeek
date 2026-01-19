using Data;
using MenuOnWeek.Domain.Recipes;

namespace MenuOnWeek.Application.Recipes;

/// <summary>
/// Репозиторий для работы с ингредиентами
/// </summary>
public interface IRecipeIngredientsRepository : IBaseRepository<RecipeIngredients>
{
}
